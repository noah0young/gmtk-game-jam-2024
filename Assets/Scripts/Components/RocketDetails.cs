using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDetails : FuelDetails
{
    private Rigidbody2D rb;
    public float thrustForce = 10f;

    void Start()
    {
        CheckForAdjacentBoxes();
        
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on this GameObject.");
        }
    }
    void Update()
    {
        this.fuelRate = 0;
        if (this.fuel > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("@@@@@@@@@@@ Rocket");
                ApplyThrust();
                this.fuelRate = 5f;
            }
        }
    }
    void ApplyThrust()
    {
        rb.AddForce(Vector2.up * thrustForce, ForceMode2D.Force);
        
    }
}
