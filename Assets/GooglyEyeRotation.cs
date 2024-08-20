using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooglyEyeRotation : MonoBehaviour
{
    [SerializeField] public Sprite a; // top right
    [SerializeField] public Sprite b; // bottom left
    [SerializeField] public Sprite c; // top left
    [SerializeField] public Sprite d; // middle
    [SerializeField] public Sprite e; // bottom right

    [SerializeField] public Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        image.sprite = d;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (transform.parent.transform.GetComponent<InventorySlot>() != null)
        {
            Debug.Log("googly eye determined that it was in the grid");
            image.sprite = d;
            return;
        } */
        
        //Debug.Log(transform.rotation.eulerAngles.z + "       "+ transform.rotation.eulerAngles.z % 360);

        switch (transform.rotation.eulerAngles.z % 360)
        {
            case < 90:
                image.sprite = b;
                break;
            case < 180:
                image.sprite = c;
                break;
            case < 270:
                image.sprite = a; break;
            case < 360:
                image.sprite = e; break;
            default:
                image.sprite = d;
                break;
        }
    }
}
