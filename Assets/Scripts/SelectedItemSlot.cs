using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectedItemSlot : MonoBehaviour, IDropHandler
{
    public static bool hasBeenSelected;

    //When the drag has been dropped if it is on a prefab with this script set it as a child if there is nothing else in the component
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("On Drop");
        if(eventData.pointerDrag != null && GetComponent<RectTransform>().transform.childCount == 0) {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>());
            //This allows you to find what is contained in the selected item slot so we can find what the player has selected
            Debug.Log(GetComponent<RectTransform>().transform.GetChild(0).GetComponent<ThisCard>().cardName);
            GetComponent<RectTransform>().transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1.0f;
            GetComponent<RectTransform>().transform.GetChild(0).GetComponent<DragDrop>().enabled = false;
            hasBeenSelected = true;
           
        }
    }
}
