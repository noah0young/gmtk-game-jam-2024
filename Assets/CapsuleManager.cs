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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void set(Sprite image, int cost, bool onSale)
    {
        spriteObject.GetComponent<Image>().sprite = image;
        text.text = "$" + cost.ToString();
        if (onSale)
        {
            saleSign.SetActive(true);
        }
        else
        {
            saleSign.SetActive(false);
        }
        
    }
}
