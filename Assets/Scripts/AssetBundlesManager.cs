using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;

public class AssetBundlesManager : Singleton<AssetBundlesManager>
{
    private AssetBundle ab;

    public string assetBundleName;
    public string assetBundleURL;
    public uint abVersion;
    public string abVersionURL;

    private IEnumerator Start()
    {
        yield return StartCoroutine(GetABVersion());
        yield return StartCoroutine(LoadAssets(assetBundleName, result => ab = result));
        yield return StartCoroutine(LoadAssetsFromURL());
    }

    private IEnumerator LoadAssets(string name, Action<AssetBundle> bundle)
    {
        AssetBundleCreateRequest abcr;
        string path = Path.Combine(Application.streamingAssetsPath, name);
        abcr = AssetBundle.LoadFromFileAsync(path);
        yield return abcr;
        bundle.Invoke(abcr.assetBundle);
        Debug.LogFormat(abcr.assetBundle == null ? "Failed to Load Asset Bundle : {0}" : "Asset Bundle {0} loaded", name);
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
    private IEnumerator GetABVersion()
    {
        UnityWebRequest uwr = UnityWebRequest.Get(abVersionURL);
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.SetRequestHeader("User-Agent", "DefaultBrowser");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }

        Debug.Log(uwr.downloadHandler.text);
        abVersion = uint.Parse(uwr.downloadHandler.text);
    }
}

