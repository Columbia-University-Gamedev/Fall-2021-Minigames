using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ground;
    [SerializeField] private SpriteRenderer background;
    private bool faded = false;
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (!faded)
        {
            StartCoroutine(ImageFade.FadeSprite(true, 1f, 1f, ground));
            StartCoroutine(ImageFade.FadeSprite(true, 1f, 1f, background));
        }
        faded = true;
    }
}
