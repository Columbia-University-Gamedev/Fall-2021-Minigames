using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private static HealthUI _instance;

    public static HealthUI Instance { get { return _instance; } }

    public GameObject heart;
    private float dist = 25f;

    private List<GameObject> healthBar = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
    }

    public void InitializeHP(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            healthBar.Add(Instantiate(heart,
                Vector3.zero, Quaternion.identity,
                this.transform));
            healthBar[i].transform.localPosition = dist * (1 - i) * Vector3.right;
        }
    }

    // Update is called once per frame
    public void UpdateHP(int amount)
    {
        for(int i = 0; i < healthBar.Count; i++)
        {
            if(i >= amount)
            {
                healthBar[i].SetActive(false);
            }
        }
    }
}
