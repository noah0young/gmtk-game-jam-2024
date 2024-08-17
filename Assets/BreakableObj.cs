using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObj : MonoBehaviour
{
    private static readonly string MACHINE_TAG = "Player";
    [SerializeField] private int health = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MACHINE_TAG))
        {
            //collision.GetComponent<>
        }
    }
}
