using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Remoting.Messaging;

public class SpriteAssetLoader : Singleton<SpriteAssetLoader>
{
    public string spriteName;
    private SpriteRenderer spriteRenderer;
    private Sprite sprite;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        StartCoroutine(SetSprite());
    }

    private IEnumerator SetSprite()
    {
        sprite = Resources.Load<Sprite>(spriteName);

        if(Input.GetKeyDown(KeyCode.E))
        {
            spriteRenderer.sprite = sprite;
        }
        yield return spriteRenderer;
    }
}
