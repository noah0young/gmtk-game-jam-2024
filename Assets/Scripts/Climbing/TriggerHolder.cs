using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerHolder : MonoBehaviour
{
    public delegate void OnReachEnd();
    private OnReachEnd atEnd;
    [SerializeField] private string[] tags;

    public void AddToOnEnd(OnReachEnd atEnd)
    {
        this.atEnd += atEnd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HasTag(collision.gameObject))
        {
            if (atEnd != null)
            {
                atEnd();
            }
        }
    }

    private bool HasTag(GameObject obj)
    {
        foreach (string tag in tags)
        {
            if (obj.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
}
