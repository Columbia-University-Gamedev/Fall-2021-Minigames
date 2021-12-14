using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollected : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float initialOpacity;
    void Start()
    {
        StartCoroutine(CoinCollectedDisappear());
        sprite = GetComponent<SpriteRenderer>();
        initialOpacity = sprite.color.a;
    }

    IEnumerator CoinCollectedDisappear()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ImageFade.FadeSprite(true, 1f, 1f, sprite));
        sprite.gameObject.SetActive(false);
    }
}