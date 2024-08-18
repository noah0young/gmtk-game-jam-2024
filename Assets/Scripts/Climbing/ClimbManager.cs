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
    [SerializeField] private Transform areaBackrgoundHolder;

    [Header("Active Data")]
    private ProceduralGenerator[] proceduralGeneratorAreas;
    [Tooltip("Where to place the next ProceduralBox. Determined by the previous ProceduralBox")]
    private Vector2 nextSpotToPlace;
    private ProceduralGenerator curArea;
    private Queue<ProceduralBox> activeBoxes;
    private int numBoxesPlacedInCurArea = 0;
    private int nextAreaIndex = 0;
    private GameObject areaBackground;
    private bool allBoxesHaveBeenPlaced;

    [Header("Constants")]
    public static readonly int NUM_BOXES_OPEN = 5;
    /** The number of boxes at the beginning of the game that have no trigger to create a new box */
    public static readonly int NUM_BOX_OFFSET_NO_TRIGGER = 2;

    private void Start()
    {
        allBoxesHaveBeenPlaced = false;
        InitProceduralGeneratorAreas();
        numBoxesPlacedInCurArea = 0;
        nextAreaIndex = 0;
        activeBoxes = new Queue<ProceduralBox>();
        nextSpotToPlace = firstSpotToPlace.position;
        SwitchToNextArea();
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

    private void InitProceduralGeneratorAreas()
    {
        proceduralGeneratorAreas = new ProceduralGenerator[proceduralDataAreas.Length];
        for (int i = 0; i < proceduralDataAreas.Length; i++)
        {
            proceduralGeneratorAreas[i] = new ProceduralGenerator(proceduralDataAreas[i]);
        }
    }

    private void PlaceProceduralBoxWithTrigger()
    {
        ProceduralBox box = PlaceProceduralBox();
        if (box != null)
        {
            box.SetOnEnd(() => { this.PlaceProceduralBoxWithTrigger(); });
        }
    }

    /** Places a new ProceduralBox and returns the new instance. */
    private ProceduralBox PlaceProceduralBox()
    {
        GameObject boxPrefab;
        try
        {
            if (numBoxesPlacedInCurArea > curArea.NumBoxesInArea())
            {
                SwitchToNextArea();
            }
            numBoxesPlacedInCurArea += 1;
            boxPrefab = curArea.GetRandomBox();
            return PlaceProceduralBox(boxPrefab);
        }
        catch
        {
            if (!allBoxesHaveBeenPlaced)
            {
                boxPrefab = endProceduralBox;
                allBoxesHaveBeenPlaced = true;
                return PlaceProceduralBox(boxPrefab, true);
            }
            return null;
        }
    }

    /** Places a new ProceduralBox and returns the new instance. */
    private ProceduralBox PlaceProceduralBox(GameObject boxPrefab, bool forcePlace = false)
    {
        if (allBoxesHaveBeenPlaced && !forcePlace)
        {
            return null; // No more boxes will be placed
        }
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

    private void SwitchToNextArea()
    {
        if (nextAreaIndex >= proceduralGeneratorAreas.Length)
        {
            throw new IndexOutOfRangeException("No more areas remain");
        }
        numBoxesPlacedInCurArea = 0;
        curArea = proceduralGeneratorAreas[nextAreaIndex];
        if (areaBackground != null)
        {
            Destroy(areaBackground);
        }
        GameObject backgroundPrefab = curArea.GetBackground();
        if (backgroundPrefab != null)
        {
            areaBackground = Instantiate(backgroundPrefab);
            areaBackground.transform.SetParent(areaBackrgoundHolder);
            areaBackground.transform.position = areaBackrgoundHolder.position;
        }
        nextAreaIndex += 1;
    }

    public void MachineStopped()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BuildScene");
    }
}
