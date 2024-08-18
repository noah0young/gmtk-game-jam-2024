using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private int money;

    private Capsule one;
    private Capsule two;
    private Capsule three;

    [SerializeField] public GameObject[] possibleComponents;

    [SerializeField] public GameObject[] capsules;

    [SerializeField] public Button[] buyButtons;

    public GameObject Inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        refreshCapsules();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void refreshCapsules()
    {
        one = generateCapsule();
        two = generateCapsule();
        three = generateCapsule();
        //capsules[0].set(one.component.GetComponent<Image>().sprite, one.cost, one.onSale);
    }

    private Capsule generateCapsule()
    {
        GameObject selected = possibleComponents[Random.Range(0, possibleComponents.Length)];
        Capsule caps = new Capsule();
        caps.component = selected;
        //caps.cost = selected.GetComponent<Cost>();
        caps.onSale = false;
        if (isOnSale())
        {
            caps.cost = caps.cost / 2;
            caps.onSale = true;
        }

        return caps;
    }

    public void BuyItem(int capsule) {
        
        //GameObject go = Instantiate(,Inventory.transform);
    }

    private bool isOnSale()
    {
        return Random.Range(0, 100) < 2;
    } 
}

public class Capsule {
    public GameObject component;
    public int cost;
    public bool onSale;
}