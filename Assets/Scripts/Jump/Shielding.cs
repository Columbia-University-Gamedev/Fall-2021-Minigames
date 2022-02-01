using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielding : MonoBehaviour
{
    [SerializeField] float timer = 1f;
    [SerializeField] private float alpha;
    [SerializeField] private GameObject coinCollected;

    private Vector3 shieldSize;
    private SpriteRenderer sprite;
    

    void Start()
    {
        shieldSize = transform.localScale;
        transform.localScale = Vector3.zero;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void TriggerShield()
    {
        StartCoroutine(ShieldTimer());
    }

    IEnumerator ShieldTimer()
    {
        StartCoroutine(ImageFade.FadeSprite(true, timer, alpha, sprite));
        
        for (float i = 0; i <= timer; i += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, shieldSize, Time.deltaTime);
            yield return null;
        }
        transform.localScale = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            var monster = other.gameObject.GetComponent<MonsterMove>();

            monster.TriggerKill();

            dummy_movement.count += 1;
            Vector3 monsterPos = other.transform.position + Vector3.up;
            Instantiate(coinCollected, monsterPos, Quaternion.identity);

            // Debug.Log("Shielded");

        }
    }
}