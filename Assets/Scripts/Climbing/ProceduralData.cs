using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProceduralData", menuName = "Procedural Data Stuff/ProceduralData")]
public class ProceduralData : ScriptableObject
{
    [SerializeField] private GameObject areaBackground;
    [SerializeField] private GameObject[] boxOptions;
    [SerializeField] private int numBoxesInArea = 5;

    public GameObject GetRandomBox()
    {
        return boxOptions[Random.Range(0, boxOptions.Length)];
    }

    public int NumBoxesInArea()
    {
        return numBoxesInArea;
    }

    public GameObject GetBackground()
    {
        return areaBackground;
    }
}
