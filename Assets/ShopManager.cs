using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{

    [SerializeField] public List<GameObject> possibleComponents;

    [SerializeField] public CapsuleManager[] allCapsules = new CapsuleManager[3];
    
    [SerializeField] public GameObject inventory;

    [SerializeField] public GameObject screen;
    
    [SerializeField] public AudioManager audioManager;
    
    private int money = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        //capsules = transform.GetComponentsInChildren<CapsuleManager>();
        RefreshCapsules();
    }

    private void RefreshCapsules()
    {
        GameObject temp = randomComponent();
        Debug.Log(temp);
        int tempCost = getCostForComponent(temp);
        bool tempSale = isOnSale();
        if (tempSale) {
            tempCost /= 2;
        }
        allCapsules[0].Set(temp, temp.GetComponent<Image>().sprite, tempCost, tempSale);
        temp = randomComponent();
        tempCost = getCostForComponent(temp);
        tempSale = isOnSale();
        if (tempSale)
        {
            tempCost /= 2;
        }
        allCapsules[1].Set(temp, temp.GetComponent<Image>().sprite, tempCost, tempSale);
        temp = randomComponent();
        tempCost = getCostForComponent(temp);
        tempSale = isOnSale();
        if (tempSale)
        {
            tempCost /= 2;
        }
        allCapsules[2].Set(temp, temp.GetComponent<Image>().sprite, tempCost, tempSale);
    }

    private GameObject randomComponent() {
        Debug.Log(possibleComponents.Count);
        return possibleComponents[Random.Range(0, possibleComponents.Count)];
    }

    private int getCostForComponent(GameObject component) {
        int baseCost = component.GetComponent<Cost>().baseCost;
        int extra = Random.Range(-1, 2);
        return math.max(1, baseCost + extra);
    }

    private bool isOnSale() {
        return Random.Range(0, 100) < 2;
    } 
    
    public void BuyItem(CapsuleManager capsule) {
        if (capsule.cost > money) {
            // cannot buy
            screen.transform.GetChild(0).gameObject.SetActive(false);
            screen.transform.GetChild(1).gameObject.SetActive(true);
            audioManager.playDeny();
        }
        else
        {
            // purchase successful
            screen.transform.GetChild(1).gameObject.SetActive(true);
            screen.transform.GetChild(1).gameObject.SetActive(false);
            GameObject go = Instantiate(capsule.componentPrefab,inventory.transform.position, inventory.transform.rotation, inventory.transform);
            go.GetComponent<Rigidbody2D>().simulated = true;
            money -= capsule.cost;
            audioManager.playMoney();
            RefreshCapsules();
        }
    }
}
