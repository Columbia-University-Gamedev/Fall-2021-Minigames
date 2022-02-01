using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheep_appears_home_screen : MonoBehaviour
{
    [SerializeField] public GameObject sheep;
    // Start is called before the first frame update
    void Start()
    {
        sheep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1){  
            sheep.SetActive(true);
        }
    }
}
