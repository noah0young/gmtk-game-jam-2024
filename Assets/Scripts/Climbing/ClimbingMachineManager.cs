using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[Serializable]
public class MachineComponentInfo
{
    public string name;
    public GameObject machineComponentPrefab;
}

public class ClimbingMachineManager : MonoBehaviour
{
    //[SerializeField] private 
    [SerializeField] private Transform machineStartPos;
    [SerializeField] private MachineComponentInfo[] machineComponentDictionary;
    [SerializeField] private ClimbManager climbManager;

    [Header("Game State")]
    private float startingY;
    private Transform machineTracker;
    private Vector2 machinePrevLocation;
    public static readonly float TIME_BETWEEN_CHECKS = 1;
    public static readonly float MIN_DISTANCE_NEEDED = 4;

    private void Start()
    {
        BuildMachine();
    }

    private void FixedUpdate()
    {
        CheckMachineStillMoving();
        UpdateScore();
    }

    private void UpdateScore()
    {
        int score = (int)Mathf.Floor((machineTracker.position.y - startingY) * 10);
        ClimbingUI.SetScoreVal(score);
    }

    private void BuildMachine()
    {
        // todo: Read Inventory and add components
        // todo: Set startingY here
    }

    private IEnumerator CheckMachineStillMoving()
    {
        while (true)
        {
            yield return new WaitForSeconds(TIME_BETWEEN_CHECKS);
            if (Vector2.Distance(machinePrevLocation, machineTracker.position) > MIN_DISTANCE_NEEDED
                && machineTracker.position.y > machinePrevLocation.y)
            {
                // You're good
            }
            else
            {
                // Failure
                climbManager.MachineStopped();
            }
        }
    }
}
