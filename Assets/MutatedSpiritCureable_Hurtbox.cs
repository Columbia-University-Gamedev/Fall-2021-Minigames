using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutatedSpiritCureable_Hurtbox : MonoBehaviour
{

    [SerializeField] private Rigidbody2D playerRB;
    MutatedSpiritCurable parentScript;


    // Start is called before the first frame update
    void Start()
    {
        parentScript = transform.parent.GetComponent<MutatedSpiritCurable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        parentScript.curingProcess();

    }





}
