using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleManager : MonoBehaviour
{
    
    [SerializeField] public GameObject spriteObject;
    [SerializeField] public TextMeshProUGUI text;
    [SerializeField] public GameObject saleSign;
    
    [HideInInspector] public GameObject componentPrefab;
    [HideInInspector] public Sprite image;
    [HideInInspector] public int cost;
    [HideInInspector] public bool onSale;

    public void Set(GameObject compPrefab, Sprite newImage, int newCost, bool newOnSale)
    {
        componentPrefab = compPrefab;
        image = newImage;
        cost = newCost;
        onSale = newOnSale;
        spriteObject.GetComponent<Image>().sprite = image;
        text.text = "$" + cost.ToString();
        saleSign.SetActive(onSale);
    }
}
