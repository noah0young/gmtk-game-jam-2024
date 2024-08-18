using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningComponentDetails : ComponentDetails
{
    public int miningDamage;
    public float damageRate = 1.0f;
    private float nextDamageTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            BreakableObj obstacleHealth = collision.gameObject.GetComponent<BreakableObj>();

            if (obstacleHealth != null)
            {
                obstacleHealth.TakeDamage(miningDamage);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            BreakableObj obstacleHealth = collision.gameObject.GetComponent<BreakableObj>();
            if (Time.time >= nextDamageTime)
            {
                obstacleHealth.TakeDamage(miningDamage);
                nextDamageTime = Time.time + damageRate;
            }
        }
    }
}
