using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProceduralData", menuName = "Procedural Data Stuff/ProceduralData")]
public class ProceduralData : ScriptableObject
{
    public GameObject areaBackground;
    public GameObject[] boxOptions;
    public int numBoxesInArea = 5;
}
