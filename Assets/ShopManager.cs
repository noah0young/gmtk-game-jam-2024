using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    private int money;
    [SerializeField] public GameObject shelves;
    // Start is called before the first frame update
    void Start()
    {
        var one = new Shelf();
        var two = new Shelf();
        var three  = new Shelf();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void BuyItem(int shelf) {
        
    }
}

public class Shelf {
    private GameObject component;
    private int cost;
    private bool onSale;
}