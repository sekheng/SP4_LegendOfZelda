using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    //private GameObject[] allSlots;
    public static GameObject itemBeingDragged;//object that is gonna be dragged around
    Vector3 startPosition;//starting pos to put the opject back if failed to locate slot
    Transform startParent;
    private Transform theCanvas;
    //public character player;

    void Start()
    {
        theCanvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        //allSlots = GameObject.FindGameObjectsWithTag("InventorySlot");
        //Debug.Log(GameObject.FindGameObjectWithTag("Canvas").gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;

        transform.SetParent(theCanvas);
        transform.SetAsLastSibling();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //Debug.Log(Time.deltaTime);
        //Debug.Log(GameObject.FindGameObjectWithTag("Canvas").gameObject);
        //throw new NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount == 1)
        {
            transform.position = Input.GetTouch(0).position;
        }
        else
        {
            transform.position = Input.mousePosition;
        }
        //transform.position = Input.tou;
        //throw new NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == theCanvas)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }

        //if(transform.parent != startParent && player != null)
        //{
        //    if(startParent.GetComponent<slotstuff>().slot_type != "Inventory")
        //    {
        //        player.BroadcastMessage("removeItem", gameObject.GetComponent<itemInformation>().item_effect);
        //    }

        //}
        //GameObject temp;
        //foreach(GameObject go in allSlots)
        //{
        //    if(go.transform.childCount > 0 && go.transform.GetChild(0) == transform)//occupiedslot
        //    {
        //        break;
        //    }
        //    else if(go.transform.childCount == 0)
        //    {
        //        transform.SetParent(go.transform);
        //        transform.localPosition = Vector3.zero;
        //    }
        //}
        //throw new NotImplementedException();
    }

}
