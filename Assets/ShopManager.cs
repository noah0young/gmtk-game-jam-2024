using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
  
    
    private int money;

    private float timeScreenUpdated;
    
    private GameObject screenFail;
    private GameObject screenSuccess;
    [SerializeField] public TextMeshProUGUI screenMoney;
    
    // Start is called before the first frame update
    void Start()
    {
        //capsules = transform.GetComponentsInChildren<CapsuleManager>();
        RefreshCapsules();
        money = GameManager.Instance.totalMoney;
        screenSuccess = screen.transform.GetChild(0).gameObject;
        screenFail = screen.transform.GetChild(1).gameObject;
        screenMoney.text = "$" + money.ToString();
    }

    void Update()
    {
        float currentTime = Time.time;
        if (currentTime - timeScreenUpdated > 2f)
        {
            screenSuccess.SetActive(false);
            screenFail.SetActive(false);
            screenMoney.gameObject.SetActive(true);
        }
    }

    private void RefreshCapsules()
    {
        GameObject temp = randomComponent();
        //Debug.Log(temp);
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
        //Debug.Log(possibleComponents.Count);
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
            screenSuccess.SetActive(false);
            screenFail.SetActive(true);
            screenMoney.gameObject.SetActive(false);
            timeScreenUpdated = Time.time;
            AudioManager.PlaySFX("Deny");
        }
        else
        {
            // purchase successful
            screenSuccess.SetActive(true);
            screenFail.SetActive(false);
            screenMoney.gameObject.SetActive(false);
            timeScreenUpdated = Time.time;
            GameObject go = Instantiate(capsule.componentPrefab,inventory.transform.position, inventory.transform.rotation, inventory.transform);
            go.GetComponent<Rigidbody2D>().simulated = true;
            money -= capsule.cost;
            GameManager.Instance.totalMoney = money;
            screenMoney.text = "$" + money.ToString();
            AudioManager.PlaySFX("Money");
            RefreshCapsules();
        }
    }

    public void SellItem(string itemname = "no name")
    {
        money += 1;
        GameManager.Instance.totalMoney = money;
        screenMoney.text = "$" + money.ToString();
        Debug.Log(itemname);
    }
}
