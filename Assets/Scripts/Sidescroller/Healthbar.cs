using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    private Image hpbar;

    private PlayerController pc;
    // Start is called before the first frame update
    void Start() {
        hpbar = this.GetComponent<Image>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        hpbar.fillAmount = (float)pc.health / (float)pc.max_health;
    }
}
