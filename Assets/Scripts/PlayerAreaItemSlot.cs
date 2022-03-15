using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerAreaItemSlot : MonoBehaviour, IDropHandler
{

    [SerializeField]
    private Player player;

    public void Start(){
        
    }
    public void Update()
    {
        if (SelectedItemSlot.hasBeenSelected)
        {
            for (int i = 0; i < Player.numOfCards-1; i++)
            {
                GetComponent<RectTransform>().transform.GetChild(i).GetComponent<DragDrop>().enabled = false;
                //continue;
            }
        }
        else
        {
            
               for (int i = 0; i < Player.numOfCards; i++)
                {
                    GetComponent<RectTransform>().transform.GetChild(i).GetComponent<DragDrop>().enabled = true;
            
                }
            
        }
    }
    //When the drag has been dropped if it is on a prefab with this script set it as a child. This variant of the script allows for multiple child components
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("On Drop");
        if (eventData.pointerDrag != null && GetComponent<RectTransform>())
        {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>());
            //This allows you to find what is contained in the selected item slot so we can find what the player has selected
            //Debug.Log(GetComponent<RectTransform>().transform.GetChild(0).GetComponent<ThisCard>().cardName);
            
        }
    }

   
}
