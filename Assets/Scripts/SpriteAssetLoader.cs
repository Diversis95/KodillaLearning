using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Remoting.Messaging;

public class SpriteAssetLoader : Singleton<SpriteAssetLoader>
{
    public string spriteName;

    SpriteRenderer sprite;

    private void Update()
    {
        sprite = GetComponent<SpriteRenderer>();

        SetSprite();
    }

    void SetSprite()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            sprite.sprite = AssetBundlesManager.Instance.GetSprite(spriteName);
        }
    }
}