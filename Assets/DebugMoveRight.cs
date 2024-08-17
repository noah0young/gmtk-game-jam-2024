using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DebugMoveRight : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    [SerializeField] private float speed;

    private void Start()
    {
        this.myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 velocity = this.myRigidbody.velocity;
        velocity.x = speed;
        this.myRigidbody.velocity = velocity;
    }
}
