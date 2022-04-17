using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyoverDraw : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void OnInRange()
    {
        Debug.Log("NPC Perspective: In range");
        panel.SetActive(true);
    }

    public void OnLeaveRange()
    {
        Debug.Log("NPC Perspective: Out of range");
        panel.SetActive(false);
    }
}
