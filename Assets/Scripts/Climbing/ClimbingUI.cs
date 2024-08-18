using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClimbingUI : MonoBehaviour
{
    private static ClimbingUI instance;
    [SerializeField] private TMP_Text scoreValText;

    private void Start()
    {
        if (instance != null)
        {
            throw new System.Exception("Only one UI can exist");
        }
        else
        {
            instance = this;
        }
    }

    public static void SetScoreVal(int val)
    {
        instance.scoreValText.text = val + "";
    }
}
