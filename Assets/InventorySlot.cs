using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {
    [SerializeField] public List<Sprite> imageList;
    public Image image;
    
    // Start is called before the first frame update
    void Start() {
        int index = Random.Range(0, imageList.Count - 1);
        image.sprite = imageList[index];
    }

    // Update is called once per frame
    void Update() {
        if (transform.childCount == 0) {
            image.enabled = true;
        }
        else {
            image.enabled = false;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0) {
            GameObject item = eventData.pointerDrag;
            DraggableItem draggableItem = item.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
            draggableItem.body.simulated = false;
        }
    }
}
