using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class RocketDetails : FuelDetails
{
    private Rigidbody2D rb;
    public float thrustForce = 10f;
    private GameInput input;
    private bool flying = false;
    private bool canFly = true;
    [SerializeField] private ParticleSystem rocketParticles;

    void Start()
    {
        canFly = true;
        input = new GameInput();
        input.Player.RocketFly.Enable();
        input.Player.RocketFly.performed += (e) => { flying = true; };
        input.Player.RocketFly.canceled += (e) => { flying = false; };
        CheckForAdjacentBoxes();
        if (rocketParticles != null)
        {
            StartCoroutine(RocketParticleRoutine());
        }
        
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on this GameObject.");
        }
    }

    private void OnDestroy()
    {
        input.Player.RocketFly.Disable();
    }

    private IEnumerator RocketParticleRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => ShouldFly());
            rocketParticles.Play();
            yield return new WaitUntil(() => !ShouldFly());
            rocketParticles.Stop();
        }
    }

    void Update()
    {
        if (ShouldFly())
        {
            ApplyThrust();
        }
    }

    public override void outOfBattery()
    {
        base.outOfBattery();
        canFly = false;
    }

    private bool ShouldFly()
    {
        return canFly && flying;
    }
    
    public override float GetFuelRate()
    {
        Debug.Log("Flying rate check");
        if (flying)
        {
            Debug.Log("Flying rate of " + base.GetFuelRate());
            return base.GetFuelRate();
        }
        else
        {
            return 0;
        }
    }

    void ApplyThrust()
    {
        rb.AddForce(Vector2.up * thrustForce, ForceMode2D.Force);
        
    }
}
