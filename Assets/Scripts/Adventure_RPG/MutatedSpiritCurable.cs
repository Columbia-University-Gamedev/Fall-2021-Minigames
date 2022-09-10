using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutatedSpiritCurable : MonoBehaviour
{

    private GameObject child;
    [SerializeField] private GameObject companionAnim;
    [SerializeField] float timer = 1f;
    [SerializeField] private float alpha;
    [SerializeField] private Vector3 animSize;
    [SerializeField] private StoryManager StoryManager;
    [SerializeField] private int eventNumber;
    private Animator anim;
    private Task task;

    [SerializeField] private ParticleSystem ember;
    [SerializeField] private ParticleSystem rain;
    [SerializeField] private ParticleSystem pollen;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        task = GetComponent<Task>();
        child = GameObject.Find("MutatedSpirit - Hurtbox");



    }

    //Curing Process (animations & ai change)
    public void curingProcess()
    {

        //task.completed = true;

        if (pollen != null)
        {
            pollen.Stop();
        }
        //Run the 
        StartCoroutine(CureSequence());


        //Change AI (remove hit box)\
        //only applies to mutated spirit
        if (child != null)
        {
            child.GetComponent<BoxCollider2D>().enabled = false;
            child.GetComponent<MutatedSpiritCureable_Hurtbox>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        
        

    }




    public IEnumerator CureSequence()
    {
        Debug.Log("In Curing");
        StoryManager.PerformEvent(eventNumber);
        for (float i = 0; i <= timer; i += Time.deltaTime)
        {
            companionAnim.transform.localScale = Vector3.Lerp(companionAnim.transform.localScale, animSize, Time.deltaTime);
            yield return null;
        }
        anim.SetTrigger("cured");
        yield return StartCoroutine(ImageFade.FadeSprite(true, timer, alpha, companionAnim.GetComponent<SpriteRenderer>()));
        
        companionAnim.transform.localScale = Vector3.zero;

        if (ember != null && rain != null)
        {
            ember.Stop();
            rain.Play();
        }
    }
}
