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
    [SerializeField] private CinemachineVirtualCamera vcm;
    [SerializeField] private Transform machineStartPos;
    [SerializeField] private MachineComponentInfo[] machineComponentDictionary;
    [SerializeField] private ClimbManager climbManager;
    [SerializeField] private GameObject fakeMachine; // DEBUG

    [Header("Game State")]
    private float startingY;
    private Transform machineTracker;
    private Vector2 machinePrevLocation;
    public static readonly float TIME_BETWEEN_CHECKS = 1;
    public static readonly float MIN_DISTANCE_NEEDED = 1;
    public static readonly float MIN_Y_DISTANCE = .5f;
    public static readonly float START_DELAY = 3;

    private void Start()
    {
        StartCoroutine(MachineRunner());
    }

    private void FixedUpdate()
    {
        
        //UpdateScore();
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
        machineTracker = fakeMachine.transform;
        vcm.Follow = machineTracker;
        this.startingY = machineTracker.transform.position.y;
    }

    private IEnumerator MachineRunner()
    {
        BuildMachine();
        bool running = true;

        // A start delay is needed due to the initial drop of placing the machine
        yield return new WaitForSeconds(START_DELAY);

        // Checks if the machine is still moving up
        while (running)
        {
            yield return new WaitForSeconds(TIME_BETWEEN_CHECKS);
            float distanceTraveled = Vector2.Distance(machinePrevLocation, machineTracker.position);
            Debug.Log("distanceTraveled = " + distanceTraveled);
            Debug.Log("Cur Y Pos = " + machineTracker.position.y);
            Debug.Log("Prev Y Pos = " + machinePrevLocation.y);
            if (distanceTraveled >= MIN_DISTANCE_NEEDED
                && machineTracker.position.y >= machinePrevLocation.y)
            {
                // You're good
            }
            else
            {
                // Failure
                MachineStopped();
                running = false;
            }
            machinePrevLocation = machineTracker.position;
        }
    }

    private void MachineStopped()
    {
        climbManager.MachineStopped();
    }
}
