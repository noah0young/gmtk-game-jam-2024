
using UnityEngine;

public class ComponentDetails : MonoBehaviour
{
    public float distanceThreshold = 1.1f;
    public float fuelRate = 0f;

    void Start()
    {
        CheckForAdjacentBoxes();
    }
    void CheckForAdjacentBoxes()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Component");

        foreach (var otherObject in taggedObjects)
        {
            if (otherObject != gameObject)
            {

                float distance = Vector2.Distance(transform.position, otherObject.transform.position);
                if (Mathf.Abs(distance) < distanceThreshold)
                {
                    AddJoint(otherObject);
                }
            }
        }
    }
    public virtual void AddJoint(GameObject otherBox)
    {
        FixedJoint2D joint = GetComponent<FixedJoint2D>();
        if (joint == null)
        {
            joint = gameObject.AddComponent<FixedJoint2D>();
        }

        joint.connectedBody = otherBox.GetComponent<Rigidbody2D>();
    }
}
