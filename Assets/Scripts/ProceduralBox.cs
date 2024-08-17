using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBox : MonoBehaviour
{
    [Tooltip("Where to place the next ProceduralBox")]
    [SerializeField] private Transform nextToPlace;

    public Vector2 NextSpot()
    {
        return nextToPlace.position;
    }
}