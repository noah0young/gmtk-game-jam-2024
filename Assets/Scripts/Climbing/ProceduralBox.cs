using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProceduralData;

public class ProceduralBox : MonoBehaviour
{
    [Tooltip("Where to place the next ProceduralBox")]
    [SerializeField] private Transform nextToPlace;
    [SerializeField] private TriggerHolder atEndTrigger;
    

    public Vector2 NextSpot()
    {
        return nextToPlace.position;
    }

    public void SetOnEnd(TriggerHolder.OnReachEnd atEnd)
    {
        this.atEndTrigger.SetOnEnd(atEnd);
    }
}