using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private ProceduralData[] proceduralDataAreas;
    [SerializeField] private GameObject endProceduralBox;

    [Header("Starting Data")]
    [SerializeField] private Transform firstSpotToPlace;

    [Header("Active Data")]
    [Tooltip("Where to place the next ProceduralBox. Determined by the previous ProceduralBox")]
    private Vector2 nextSpotToPlace;
    private ProceduralData curArea;
    private Queue<ProceduralBox> activeBoxes;
    public static readonly int NUM_BOXES_OPEN = 5;

    private void Start()
    {
        curArea = proceduralDataAreas[0];
    }

    private void PlaceProceduralBox()
    {
        GameObject boxPrefab = curArea.GetRandomBox();
        GameObject boxObj = Instantiate(boxPrefab);
        boxObj.transform.position = nextSpotToPlace;
        ProceduralBox box = boxObj.GetComponent<ProceduralBox>();
        activeBoxes.Enqueue(box);
        this.nextSpotToPlace = box.NextSpot();
    }

    private void TryDestroyOld()
    {
        if (activeBoxes.Count > NUM_BOXES_OPEN)
        {

        }
    }
}
