using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject grid;

    public GameObject inventory;

    public ComponentConversion[] conversionDictionary;
    
    
    // Start is called before the first frame update
    private void Start() {
        if (GameManager.Instance != null)
        {
            // Load machine into the grid
            foreach (var component in GameManager.Instance.Inmachine)
            {
                GameObject go = Instantiate(findComponentConversion(component.type), Vector2.zero, component.rotation, grid.transform.GetChild(component.locationInGrid));
                go.GetComponent<Rigidbody2D>().simulated = false;
            }

            // load remaining items into the inventory
            foreach (var component in GameManager.Instance.Ininventory)
            {
                GameObject go = Instantiate(findComponentConversion(component.type), component.position, component.rotation, inventory.transform);
                go.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Save() {
        int numComponentsInInventory = inventory.transform.childCount;
        GameManager.Instance.Ininventory = new List<Component>();
        DraggableItem[] allDragableItems = inventory.GetComponentsInChildren<DraggableItem>();
        foreach (DraggableItem item in allDragableItems)
        { 
            var obj = item.gameObject;
            var com = new Component
            {
                locationInGrid = -1,
                position = obj.transform.position,
                rotation = obj.transform.rotation,
                type = item.GetComponent<Cost>().componentType
            };
            GameManager.Instance.Ininventory.Add(com);
        }
        // all the stuff in the grid
        GameManager.Instance.Inmachine = new List<Component>();
        for (int i = 0; i < 100; i++)
        {
            if (grid.transform.GetChild(i).childCount <= 0) continue;
            var com = new Component
            {
                locationInGrid = i,
                rotation = grid.transform.GetChild(i).GetChild(0).transform.rotation,
                type = grid.transform.GetChild(i).GetChild(0).GetComponent<Cost>().componentType
            };
            GameManager.Instance.Inmachine.Add(com);
        }
        // DEBUG
        foreach (Component c in GameManager.Instance.Ininventory)
        {
            Debug.Log("Inventory has " + c.type);
        }
        foreach (Component c in GameManager.Instance.Inmachine)
        {
            Debug.Log("Machine has " + c.type);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("ClimbScene");
    }
    
    private GameObject findComponentConversion(string type)
    {
        foreach (var t in conversionDictionary)
        {
            if (t.name == type)
            {
                return t.prefab;
            }
        }

        throw new Exception("Component not found, could not find " + type);
    }
}

public class Component
{
    // -1 means in inventory, 0-99 means in grid
    public int locationInGrid;
    public string type;
    public Vector3 position;
    public Quaternion rotation;

    public Vector2 ClimbingPosition()
    {
        return new Vector2(locationInGrid % 10, 10 - locationInGrid / 10);
    }
}

[Serializable] public class ComponentConversion
{
    public string name;
    public GameObject prefab;
}