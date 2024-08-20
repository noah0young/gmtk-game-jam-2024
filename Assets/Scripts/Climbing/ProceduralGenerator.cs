using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator
{
    private ProceduralData data;

    [Header("Random Weighting")]
    private int[] timesBoxUsed;
    private int prevBoxIndexUsed;

    public ProceduralGenerator(ProceduralData data)
    {
        this.data = data;
        timesBoxUsed = new int[data.boxOptions.Length];
        prevBoxIndexUsed = -1;
    }

    private int SelectRandomIndex()
    {
        int[] numTimesNeededToBeRolled = new int[timesBoxUsed.Length];
        string arrayDebug = "[ "; // DEBUG
        for (int i = 0; i < timesBoxUsed.Length; i++)
        {
            numTimesNeededToBeRolled[i] = timesBoxUsed[i];
            arrayDebug += timesBoxUsed[i] + " "; // DEBUG
        }
        Debug.Log(arrayDebug + "]"); // DEBUG
        int selected;
        bool success = false;
        do
        {
            selected = Random.Range(0, data.boxOptions.Length);
            if (numTimesNeededToBeRolled[selected] > 0)
            {
                numTimesNeededToBeRolled[selected] -= 1;
            }
            else if (selected == prevBoxIndexUsed)
            {
                // Do not use
            }
            else
            {
                success = true;
            }
        }
        while (!success);
        return selected;
    }

    private void IncreaseBoxUsedIndexOf(int index)
    {
        timesBoxUsed[index] += 1;
        RefreshTimesBoxUsedArray();
    }

    private void RefreshTimesBoxUsedArray()
    {
        int minUsedVal = timesBoxUsed[0];
        for (int i = 0; i < timesBoxUsed.Length; i++)
        {
            minUsedVal = Mathf.Min(minUsedVal, timesBoxUsed[i]);
        }
        for (int i = 0; i < timesBoxUsed.Length; i++)
        {
            timesBoxUsed[i] -= minUsedVal;
        }
    }

    public GameObject GetRandomBox()
    {
        int selectedIndex = SelectRandomIndex();
        prevBoxIndexUsed = selectedIndex;
        IncreaseBoxUsedIndexOf(selectedIndex);
        return data.boxOptions[selectedIndex];
    }

    public int NumBoxesInArea()
    {
        return data.numBoxesInArea;
    }

    public GameObject GetBackground()
    {
        return data.areaBackground;
    }

    public Color GetBackgroundColor()
    {
        return data.backgroundColor;
    }
}
