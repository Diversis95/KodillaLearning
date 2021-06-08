using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class AssetBundlesManager : Singleton<AssetBundlesManager>
{
    private AssetBundle ab;

    public string assetBundleName;
    public string assetBundleURL;
    public uint abVersion;

    private void Start()
    {
        StartCoroutine(LoadAssets());
    }

    private IEnumerator LoadAssets()
    {
        AssetBundleCreateRequest abcr;
        string path = Path.Combine(Application.streamingAssetsPath, assetBundleName);
        abcr = AssetBundle.LoadFromFileAsync(path);

        yield return abcr;

        ab = abcr.assetBundle;

        Debug.Log(ab == null ? "Failed to load Asset Bundle" : "Asset Bundle loaded");

    }

    private IEnumerator LoadAssetsFromURL()
    {
        UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleURL, abVersion, 0);

        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            ab = DownloadHandlerAssetBundle.GetContent(uwr);
        }

        Debug.Log(ab == null ? "Failed to download Asset Bundle" : "Asset Bundle downloaded");
        Debug.Log("Downloaded bytes : " + uwr.downloadedBytes);
    }

    public Sprite GetSprite(string assetName)
    {
        return ab.LoadAsset<Sprite>(assetName);
    }
}

