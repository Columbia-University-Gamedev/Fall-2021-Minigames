using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    public PlayerController pc;
    public Image hpbar;
    public Color healthyColor;
    public Color mediumColor;
    public Color dangerColor;
    
    // Start is called before the first frame update
    void Start() {
        hpbar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update() {
        hpbar.fillAmount = Mathf.Lerp(hpbar.fillAmount,
            (float) pc.health / (float) pc.maxHealth,
            Time.deltaTime*5f); //makes it look nice and animated
        
        if (hpbar.fillAmount > 0.5f) {
            hpbar.color = Color.Lerp(mediumColor, 
                healthyColor, (hpbar.fillAmount - 0.5f)*2.0f);
        }
        else {
            hpbar.color = Color.Lerp(dangerColor, 
                mediumColor, hpbar.fillAmount*2.0f);
        }
    }
}
