using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BreakableObj : MonoBehaviour
{
    private static readonly string MACHINE_ATTACK_TAG = "Finish";
    [SerializeField] private int health = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MACHINE_ATTACK_TAG))
        {
            TakeDamage(health);
            /*MachineDamageDealer damageDealer = collision.GetComponent<MachineDamageDealer>();
            TakeDamage(damageDealer.AttackPower());*/
        }
    }

    protected virtual void TakeDamage(int dmg)
    {
        health -= dmg;
        health = Mathf.Max(health, 0);
        if (health <= 0)
        {
            Break();
        }
    }

    protected abstract void Break();
}
