using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutatedSpiritCurable : MonoBehaviour
{

    private string[] states = { "Corrupted", "Cured" };
    private string currentState;
    private GameObject child;
    [SerializeField] private GameObject companionAnim;
    [SerializeField] float timer = 1f;
    [SerializeField] private float alpha;
    [SerializeField] private Vector3 animSize;
    private Task task;

    // Start is called before the first frame update
    void Start()
    {

        task = GetComponent<Task>();
        currentState = states[0];
        child = GameObject.Find("MutatedSpirit - Hurtbox");



    }

    //Curing Process (animations & ai change)
    public void curingProcess()
    {

        //task.completed = true;

        //Run the 
        StartCoroutine(CureSequence());

        //Change animation
        currentState = states[1];

        //Change AI (remove hit box)\
        child.GetComponent<BoxCollider2D>().enabled = false;
        child.GetComponent<MutatedSpiritCureable_Hurtbox>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        
        

    }




    public IEnumerator CureSequence()
    {
        Debug.Log("In Curing");
        for (float i = 0; i <= timer; i += Time.deltaTime)
        {
            companionAnim.transform.localScale = Vector3.Lerp(companionAnim.transform.localScale, animSize, Time.deltaTime);
            yield return null;
        }
        yield return StartCoroutine(ImageFade.FadeSprite(true, timer, alpha, companionAnim.GetComponent<SpriteRenderer>()));

        companionAnim.transform.localScale = Vector3.zero;
    }
}
