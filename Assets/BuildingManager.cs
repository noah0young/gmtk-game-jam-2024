using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
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
