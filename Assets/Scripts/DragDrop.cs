using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Most of the code uses the interfaces implemented to track mouse movements and if it is being held or not
    //Drag and Drop class
   private GameObject canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    
    //rectTransform store and change the position of an object/prefab
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("Canvas");
    }
    //Once something begins to drag, lower opacity and prevent it from changing the canvas in this state
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        transform.SetParent(canvas.transform, true);
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }


    //While it is dragging follow the mouse coordinates
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        Debug.Log (canvas.GetComponent<RectTransform>().sizeDelta);
       // float scaler =  (1920*1080) / (canvas.GetComponent<RectTransform>().sizeDelta.x * canvas.GetComponent<RectTransform>().sizeDelta.y);
       // Debug.Log.(canvas.GetComponent<Canvas>().
        
        rectTransform.anchoredPosition += eventData.delta;
       
    }

    //End the drag and reset opacity and canvas interaction.
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

       
    }

    //Check if it is detected on the canvas
   // public void OnPointerDown(PointerEventData eventData)
  //  {
   //     Debug.Log("OnPointerDown");

   // }

    
}

