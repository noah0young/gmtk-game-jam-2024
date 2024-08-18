using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    public float totalFuelRate = .1f;
    public float totalFuel;
    // Start is called before the first frame update
    void Start()
    {
        totalFuel = getTotalFuel();
    }

    // Update is called once per frame
    void Update()
    {
        if (totalFuel > 0)
        {
            depleteFuel();
        }
        else
        {
            Debug.Log("you died");
        }
    }

    float getTotalFuel()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Component");
        float totalFuel = 0f;
        foreach (var otherObject in taggedObjects)
        {
            FuelDetails obj = otherObject.GetComponent<FuelDetails>();
            if (obj != null)
            {
                totalFuel += obj.fuel;
            }
        }
        return totalFuel;
    }

    void depleteFuel()
    {
        totalFuel -= totalFuelRate * Time.deltaTime;
    }
}
