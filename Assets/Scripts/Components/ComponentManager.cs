using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    private GameObject[] taggedObjects; 
    public float totalFuel;
    private float origTotalFuel;
    // Start is called before the first frame update
    void Start()
    {
        this.taggedObjects = getTaggedObjects();
        totalFuel = getTotalFuel();
        origTotalFuel = totalFuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalFuel > 0)
        {
            depleteFuel();
            ClimbingUI.SetBatteryVal(totalFuel, origTotalFuel);
        }
        else
        {
            foreach (GameObject component in taggedObjects)
            {
                component.GetComponent<ComponentDetails>().outOfBattery();
                Debug.Log("Out of batery");
            }
        }
    }


    float getTotalFuel()
    {
        float totalFuel = 0f;
        foreach (var otherObject in this.taggedObjects)
        {
            FuelDetails obj = otherObject.GetComponent<FuelDetails>();
            if (obj != null)
            {
                totalFuel += obj.fuel;
            }
        }
        return totalFuel;
    }

    float getTotalFuelRate()
    {
        float totalFuelRate = 0f;
        foreach (var otherObject in this.taggedObjects)
        {
            ComponentDetails obj = otherObject.GetComponent<ComponentDetails>();
            if (obj != null)
            {
                totalFuelRate += obj.fuelRate;
            }
        }
        return totalFuelRate;
    }

    GameObject[] getTaggedObjects()
    {
        List<GameObject> taggedObjects = new List<GameObject>();
        string[] tags = new string[]{ "Component", "Wheel" };
        foreach(string i in tags)
        {
            taggedObjects.AddRange(GameObject.FindGameObjectsWithTag(i));
        }

        return taggedObjects.ToArray();
    }
    void depleteFuel()
    {
        totalFuel -= getTotalFuelRate() * Time.deltaTime;
        Debug.Log(totalFuel);
    }
}
