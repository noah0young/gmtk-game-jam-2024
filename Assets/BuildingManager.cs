using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private Component[] Inmachine;
    private Component[] Ininventory;

    public GameObject grid;

    public GameObject inventory;
    
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void save() {
        int numComponentsInInventory = inventory.transform.childCount;
        for (int i = 0; i < numComponentsInInventory; i++) {
            var obj = inventory.transform.GetChild(i).gameObject;
            var com = new Component
            {
                locationInGrid = -1,
                position = obj.transform.position,
                rotation = obj.transform.rotation,
                type = grid.transform.GetChild(i).gameObject.name
            };
            Ininventory[i] = com;
        }
        // all the stuff in the grid
        for (int i = 0; i < 100; i++)
        {
            if (grid.transform.GetChild(i).childCount <= 0) continue;
            var com = new Component
            {
                locationInGrid = i,
                type = grid.transform.GetChild(i).gameObject.name
            };
            Inmachine[Inmachine.Length] = com;
        }
    }
}

public class Component
{
    // -1 means in inventory, 0-99 means in grid
    public int locationInGrid;
    public string type;
    public Vector3 position;
    public Quaternion rotation;
}
