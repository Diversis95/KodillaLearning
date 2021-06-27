using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;

public class AssetBundlesManager : Singleton<AssetBundlesManager>
{
    public Button changeSpriteButton;
    public Button changeSceneButton;
    public SpriteRenderer sprite;
    private AssetBundle abSprite;
    private AssetBundle abScene;

    public string spriteName;
    public string sceneName;
    public string assetBundleName;
    public string assetBundleURL;

    private void Start()
    {
        StartCoroutine(LoadAssets());
        changeSpriteButton.onClick.AddListener(delegate { SetSprite(); });

        StartCoroutine(LoadAssetsFromURL());
        changeSceneButton.onClick.AddListener(delegate { SetScene(); });
    }

    private IEnumerator LoadAssets()
    {
        AssetBundleCreateRequest abcr;
        string path = Path.Combine(Application.streamingAssetsPath, assetBundleName);
        abcr = AssetBundle.LoadFromFileAsync(path);
        yield return abcr;
        abSprite = abcr.assetBundle;
        Debug.Log(abSprite == null ? "Failed to load Asset Bundle" : "Asset Bundle loaded");
    }

    public Sprite GetSprite(string assetName)
    {
        return abSprite.LoadAsset<Sprite>(assetName);
    }

    void SetSprite()
    {
        sprite.sprite = GetSprite(spriteName);
    }

    //--------------------------------------------------------------------------------------------------------------------------------

    private IEnumerator LoadAssetsFromURL()
    {
        UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleURL);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            abScene = DownloadHandlerAssetBundle.GetContent(uwr);
        }

        Debug.Log(abScene == null ? "Failed to download Asset Bundle" : "Asset Bundle downloaded");
    }

    void SetScene()
    {
        string[] scenePath = abScene.GetAllScenePaths();
        SceneManager.LoadScene(scenePath[0]);
    }
}

