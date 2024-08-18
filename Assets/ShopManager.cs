using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    private int money;

    [SerializeField] public GameObject[] possibleComponents;

    [SerializeField] public CapsuleManager[] capsules;

    [SerializeField] public Button[] buyButtons;

    public GameObject Inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        RefreshCapsules();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void RefreshCapsules()
    {
        GameObject temp = randomComponent();
        int tempCost = getCostForComponent(temp);
        bool tempSale = isOnSale();
        if (tempSale)
        {
            tempCost /= 2;
        }
        capsules[0].Set(temp, temp.GetComponent<Image>().sprite, tempCost, tempSale);
        temp = randomComponent();
        tempCost = getCostForComponent(temp);
        tempSale = isOnSale();
        if (tempSale)
        {
            tempCost /= 2;
        }
        capsules[1].Set(temp, temp.GetComponent<Image>().sprite, tempCost, tempSale);
        temp = randomComponent();
        tempCost = getCostForComponent(temp);
        tempSale = isOnSale();
        if (tempSale)
        {
            tempCost /= 2;
        }
        capsules[2].Set(temp, temp.GetComponent<Image>().sprite, tempCost, tempSale);
    }

    private GameObject randomComponent()
    {
        return possibleComponents[Random.Range(0, possibleComponents.Length)];
    }

    private int getCostForComponent(GameObject component)
    {
        int baseCost = component.GetComponent<Cost>().baseCost;
        int extra = Random.Range(-1, 2);
        return math.min(1, baseCost + extra);
    }

    private bool isOnSale()
    {
        return Random.Range(0, 100) < 2;
    } 
    
    public void BuyItem(CapsuleManager capsule) {
        
        GameObject go = Instantiate(capsule.componentPrefab,Inventory.transform);
        RefreshCapsules();
    }
}
