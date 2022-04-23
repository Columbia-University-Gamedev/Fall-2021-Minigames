using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureSpirit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject companionAnim;
    //[SerializeField] private SpriteRenderer mutatedSpirit;
    //[SerializeField] private Sprite curedSpirit;
    [SerializeField] float timer = 1f;
    [SerializeField] private float alpha;
    [SerializeField] private Vector3 animSize;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Animator mutatedSpirit;
    private bool cured;

    [SerializeField] private ParticleSystem ember;
    [SerializeField] private ParticleSystem rain;
    [SerializeField] private PollenProjectiles p;

    void Start()
    {
        cured = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("approachignspirit");
        Debug.Log(p);
    }
    */

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !cured)
        {

            p.stillMutated = false;

            if (playerRb.velocity.x >= 0f)
            {
                StartCoroutine(CureSequence());
            }
        }
    }

    public IEnumerator CureSequence()
    {
        for (float i = 0; i <= timer; i += Time.deltaTime)
        {
            companionAnim.transform.localScale = Vector3.Lerp(companionAnim.transform.localScale, animSize, Time.deltaTime);
            yield return null;
        }
        mutatedSpirit.SetTrigger("cured");
        //mutatedSpirit.sprite = curedSpirit;
        yield return StartCoroutine(ImageFade.FadeSprite(true, timer, alpha, companionAnim.GetComponent<SpriteRenderer>()));

        companionAnim.transform.localScale = Vector3.zero;
        cured = true;

        if (ember != null && rain != null)
        {
            ember.Stop();
            rain.Play();
        }
    }
}
