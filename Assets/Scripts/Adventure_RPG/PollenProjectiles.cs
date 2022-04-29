using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenProjectiles : MonoBehaviour
{
    ParticleSystem ps;
    public bool stillMutated;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    
        if (numEnter > 0)
        {
            Debug.Log("entered");
            PlayerMovement.TakeDamage();
        }
    }
}
