using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Remoting.Messaging;

public class SpriteAssetLoader : MonoBehaviour
{
    public string spriteName;

    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
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