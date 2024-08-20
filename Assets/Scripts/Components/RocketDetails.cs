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
        this.fuel = 10;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on this GameObject.");
        }
    }
    void Update()
    {
        if (this.fuel > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ApplyThrust();
                this.fuel -= this.fuelRate * Time.deltaTime;
                this.fuel = Mathf.Max(this.fuel, 0f);
            }
        }
        
    }
    void ApplyThrust()
    {
        rb.AddForce(Vector2.up * thrustForce, ForceMode2D.Force);
        
    }
}
