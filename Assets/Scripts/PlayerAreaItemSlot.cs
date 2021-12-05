using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerAreaItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("On Drop");
        if (eventData.pointerDrag != null && GetComponent<RectTransform>())
        {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>());
            Debug.Log(GetComponent<RectTransform>().transform.GetChild(0).GetComponent<ThisCard>().cardName);
        }
    }
}
