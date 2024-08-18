using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBox : MonoBehaviour
{
    [Tooltip("Where to place the next ProceduralBox")]
    [SerializeField] private Transform nextToPlace;
    [SerializeField] private TriggerHolder atEndTrigger;
    private bool atEndTriggerIsSet;

    private void Awake()
    {
        atEndTriggerIsSet = false;
    }

    public Vector2 NextSpot()
    {
        return nextToPlace.position;
    }

    public void SetOnEnd(TriggerHolder.OnReachEnd atEnd)
    {
        if (!atEndTriggerIsSet)
        {
            atEndTriggerIsSet = true;
            this.atEndTrigger.AddToOnEnd(atEnd);
            this.atEndTrigger.AddToOnEnd(() => Destroy(atEndTrigger.gameObject));
        }
        else
        {
            throw new System.Exception("Tried to set OnEnd twice");
        }
    }
}