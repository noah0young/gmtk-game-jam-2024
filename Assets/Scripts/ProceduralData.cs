using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProceduralData", menuName = "Procedural Data Stuff/ProceduralData")]
public class ProceduralData : ScriptableObject
{
    [SerializeField] private GameObject[] boxOptions;

    public GameObject GetRandomBox()
    {
        return boxOptions[Random.Range(0, boxOptions.Length)];
    }
}
