using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Image hpbar;
    public Image delayIndicator;

    private PlayerController pc;
    // Start is called before the first frame update
    void Start() {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        hpbar.fillAmount = (float)pc.health / (float)pc.max_health;
        delayIndicator.fillAmount = Mathf.Clamp(
            pc.timeSinceLastShot / pc.shotDelay,0f,1f);
    }
}
