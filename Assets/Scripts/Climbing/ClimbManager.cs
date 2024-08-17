using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameObject firstProceduralBox;
    [SerializeField] private ProceduralData[] proceduralDataAreas;
    [SerializeField] private GameObject endProceduralBox;

    [Header("Starting Data")]
    [SerializeField] private Transform firstSpotToPlace;
    [SerializeField] private Transform areaBackrgoundPos;

    [Header("Active Data")]
    [Tooltip("Where to place the next ProceduralBox. Determined by the previous ProceduralBox")]
    private Vector2 nextSpotToPlace;
    private ProceduralData curArea;
    private Queue<ProceduralBox> activeBoxes;
    private int numBoxesPlacedInCurArea = 0;
    private int areaIndex = 0;

    [Header("Constants")]
    public static readonly int NUM_BOXES_OPEN = 5;
    /** The number of boxes at the beginning of the game that have no trigger to create a new box */
    public static readonly int NUM_BOX_OFFSET_NO_TRIGGER = 2;

    private void Start()
    {
        numBoxesPlacedInCurArea = 0;
        areaIndex = 0;
        activeBoxes = new Queue<ProceduralBox>();
        nextSpotToPlace = firstSpotToPlace.position;
        curArea = proceduralDataAreas[areaIndex];
        PlaceProceduralBox(firstProceduralBox);
        for (int i = 1; i < NUM_BOX_OFFSET_NO_TRIGGER; i++)
        {
            PlaceProceduralBox();
        }
        for (int i = 0; i < NUM_BOXES_OPEN - NUM_BOX_OFFSET_NO_TRIGGER; i++)
        {
            PlaceProceduralBoxWithTrigger();
        }
    }

    private void PlaceProceduralBoxWithTrigger()
    {
        ProceduralBox box = PlaceProceduralBox();
        box.SetOnEnd(() => { this.PlaceProceduralBoxWithTrigger(); });
    }

    /** Places a new ProceduralBox and returns the new instance. */
    private ProceduralBox PlaceProceduralBox()
    {
        if (numBoxesPlacedInCurArea > curArea.NumBoxesInArea())
        {
            numBoxesPlacedInCurArea = 0;
            curArea = GetNextArea();
        }
        GameObject boxPrefab = curArea.GetRandomBox();
        return PlaceProceduralBox(boxPrefab);
    }

    /** Places a new ProceduralBox and returns the new instance. */
    private ProceduralBox PlaceProceduralBox(GameObject boxPrefab)
    {
        GameObject boxObj = Instantiate(boxPrefab);
        boxObj.transform.position = nextSpotToPlace;
        ProceduralBox box = boxObj.GetComponent<ProceduralBox>();
        activeBoxes.Enqueue(box);
        this.nextSpotToPlace = box.NextSpot();
        while (activeBoxes.Count > NUM_BOXES_OPEN)
        {
            DestroyOldest();
        }
        return box;
    }

    private void DestroyOldest()
    {
        ProceduralBox oldest = activeBoxes.Dequeue();
        Destroy(oldest.gameObject);
    }

    private ProceduralData GetNextArea()
    {
        areaIndex += 1;
        return proceduralDataAreas[areaIndex];
    }
}
