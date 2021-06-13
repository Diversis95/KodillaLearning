using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class AssetBundleManagerForExercise : Singleton<AssetBundleManagerForExercise>
{
    private AssetBundle ab;
    public Button changeSprite;
    public SpriteRenderer sprite;
    public string spriteName;

    private void Start()
    {
        changeSprite.onClick.AddListener(delegate { SetSprite(); });
    }

    void SetSprite()
    {
        sprite.sprite = AssetBundlesManager.Instance.GetSprite(spriteName);
    }

    void ChangeScene()
    {

    }
}
