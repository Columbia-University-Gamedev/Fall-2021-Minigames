using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plus100 : MonoBehaviour
{
    [SerializeField] private float timer = 0.5f;

    private SpriteRenderer sprite;
    private Vector3 finalPos;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        finalPos = transform.position + 2f * Vector3.up; 

        StartCoroutine(AnimationSeq());
    }

    IEnumerator AnimationSeq()
    {
        StartCoroutine(ImageFade.FadeSprite(true, timer, 1f, sprite));
        
        for (float i = 0; i <= timer; i += Time.unscaledDeltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, finalPos, Time.deltaTime * 1.5f);
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
