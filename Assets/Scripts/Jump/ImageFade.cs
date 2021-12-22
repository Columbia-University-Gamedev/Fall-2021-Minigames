using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageFade : MonoBehaviour
{
    public static IEnumerator FadeSprite(bool fadeAway, float time, float alpha, SpriteRenderer sprite)
    {
        var tmp = sprite.color;

        if (fadeAway)
        {
            // loop over time second backwards
            for (float i = time; i >= 0; i -= Time.unscaledDeltaTime)
            {
                //alpha can be manipulated directly
                tmp = sprite.color;
                tmp.a = alpha * i / time;
                sprite.color = tmp; 

                yield return null;
            }

            
            tmp.a = 0;
            sprite.color = tmp;
        }

        // fade from transparent to opaque
        else
        {
            for (float i = 0; i <= time; i += Time.unscaledDeltaTime)
            {
                tmp = sprite.color;
                tmp.a = alpha * i / time;
                sprite.color = tmp;

                yield return null;
            }

            tmp = sprite.color;
            tmp.a = 1;
            sprite.color = tmp;
        }
    }
    
    public static IEnumerator FadeImage(bool fadeAway, float time, float alpha, CanvasGroup img)
    {
        if (fadeAway)
        {
            // loop over time second backwards
            for (float i = time; i >= 0; i -= Time.unscaledDeltaTime)
            {
                //alpha can be manipulated directly
                img.alpha = alpha * i / time;
                yield return null;
            }
            img.alpha = 0;
        }

        // fade from transparent to opaque
        else
        {
            for (float i = 0; i <= time; i += Time.unscaledDeltaTime)
            {
                img.alpha = alpha * i/time;
                yield return null;
            }
            img.alpha = 1;
        }

    }

    
}