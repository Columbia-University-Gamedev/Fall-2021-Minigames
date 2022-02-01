using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidScript : MonoBehaviour {
    private BasicEnemy be;
    private Transform tr;

    public float rotationSpeed;
    void Start() {
        be = this.GetComponent<BasicEnemy>();
        tr = transform;

        tr.eulerAngles = (180 * Vector3.up) + 
                         (Random.Range(0f,360f)*Vector3.forward);
        
        Animator anim = GetComponent<Animator>();
        if (anim != null) {
            AnimatorStateInfo
                state = anim.GetCurrentAnimatorStateInfo(0); //could replace 0 by any other animation layer index
            float moment = Random.Range(0f, 1f);
            anim.Play(state.fullPathHash, -1, moment);
        }
    }

    void Update() {
        tr.Rotate(Time.deltaTime*rotationSpeed*Vector3.forward);
    }
    
    void SquidFire() {
        be.Fire();
    }
}
