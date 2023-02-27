using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpriteLoader : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            StartCoroutine(GetSpriteFromWeb((s) => _spriteRenderer.sprite = s));
    }

    IEnumerator GetSpriteFromWeb(System.Action<Sprite> onSpriteLoaded)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://sqrlycode.com/public_images/ToastCat.png");
        //Wait until image was downloaded
        yield return www.SendWebRequest();

        Texture myTexture = DownloadHandlerTexture.GetContent(www);
        Sprite s = Sprite.Create((Texture2D)myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.one * 0.5f, myTexture.height);
        onSpriteLoaded(s);
    }
}