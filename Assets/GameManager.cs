using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void OnClick();

    public static GameManager Instance { get; private set; }
    public List<Component> Inmachine = new List<Component>();
    public List<Component> Ininventory = new List<Component>();
    public int totalMoney = 0;
    public bool win = false;
    [HideInInspector] public int dayCount = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
