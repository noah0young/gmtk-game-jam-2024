using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DebugMoveRight : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed = 5;

    private void Start()
    {
        this.myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 velocity = this.myRigidbody.velocity;
        velocity.x += acceleration;
        if (velocity.x > maxSpeed)
        {
            velocity.x = maxSpeed;
        }
        this.myRigidbody.velocity = velocity;
    }
}
