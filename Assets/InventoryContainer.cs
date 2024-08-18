using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryContainer : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    public void OnDrop(PointerEventData eventData) {
            GameObject item = eventData.pointerDrag;
            DraggableItem draggableItem = item.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
            draggableItem.body.simulated = true;
            draggableItem.body.gravityScale = 2;
    }
}
