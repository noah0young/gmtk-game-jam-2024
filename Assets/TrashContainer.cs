using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashContainer : MonoBehaviour, IDropHandler {
    [SerializeField] public ShopManager shopManager;
    [SerializeField] AudioManager audioManager;
    // Start is called before the first frame update
    
    public void OnDrop(PointerEventData eventData) {
        GameObject item = eventData.pointerDrag;
        DraggableItem draggableItem = item.GetComponent<DraggableItem>();
        draggableItem.parentAfterDrag = transform;
        draggableItem.body.simulated = false;
        draggableItem.gameObject.SetActive(false);
        draggableItem.transform.position = Vector3.negativeInfinity;
        shopManager.SellItem(draggableItem.name);
        audioManager.playTrash();
    }
}
