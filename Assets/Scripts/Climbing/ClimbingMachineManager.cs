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
    [SerializeField] private GameObject machineTrackerCollider;

    [Header("Game State")]
    private float startingY;
    private Transform machineTracker;
    private float prevMaxY;
    private Vector2 machinePrevLocation;
    public static readonly float TIME_BETWEEN_CHECKS = 1;
    public static readonly float MIN_DISTANCE_NEEDED = .1f;
    public static readonly float MAX_Y_FALL_DISTANCE = 10;
    public static readonly float START_DELAY = 3;

    private void Start()
    {
        StartCoroutine(MachineRunner());
    }

    private void FixedUpdate()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        int score = GetScore();
        ClimbingUI.SetScoreVal(score);
    }

    private int GetScore()
    {
        return (int)Mathf.Floor((prevMaxY - startingY) * 10);
    }

    private GameObject FindComponentPrefab(string type)
    {
        for (int i = 0; i < machineComponentDictionary.Length; i++)
        {
            if (machineComponentDictionary[i].name == type)
            {
                return machineComponentDictionary[i].machineComponentPrefab;
            }
        }
        throw new Exception("Component not found, could not find " + type);
    }

    private void BuildMachine()
    {

        foreach (Component component in GameManager.Instance.Inmachine)
        {
            GameObject obj = Instantiate(FindComponentPrefab(component.type));
            obj.transform.position = component.ClimbingPosition() + (Vector2)machineStartPos.position;
            obj.transform.rotation.SetEulerAngles(new Vector3(0, 0, component.rotation.eulerAngles.z));
            machineTracker = obj.transform;
        }
        machineTrackerCollider.transform.SetParent(machineTracker);

        vcm.Follow = machineTracker;
        this.startingY = machineTracker.transform.position.y;
        this.prevMaxY = machineTracker.transform.position.y;
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
            Debug.Log("Prev Y Pos = " + prevMaxY);
            if (distanceTraveled >= MIN_DISTANCE_NEEDED
                && (machineTracker.position.y >= machinePrevLocation.y || Mathf.Abs(machineTracker.position.y - machinePrevLocation.y) < MAX_Y_FALL_DISTANCE))
            {
                // You're good
            }
            else
            {
                // Failure
                if (!GameManager.Instance.win)
                {
                    MachineStopped();
                }
                else
                {
                    ClimbingUI.PlayYouWin();
                }
                running = false;
            }
            machinePrevLocation = machineTracker.position;
            prevMaxY = Mathf.Max(prevMaxY, machineTracker.position.y);
        }
    }

    private void MachineStopped()
    {
        climbManager.MachineStopped(GetScore());
    }
}
