using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageFade : MonoBehaviour
{
    public static IEnumerator FadeImage(bool fadeAway, float time, float alpha, Image img)
    {
        if (fadeAway)
        {
            // loop over time second backwards
            for (float i = time; i >= 0; i -= Time.deltaTime)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, alpha * i/time);
                yield return null;
            }
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        }
        // fade from transparent to opaque
        else
        {
            for (float i = 0; i <= time; i += Time.deltaTime)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, alpha * i/time);
                yield return null;
            }
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        }
    }
    
}