using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAreaItemSlot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RectTransform>().childCount > 0){
          for (int i = 0; i < GetComponent<RectTransform>().childCount; i++)
            {
                GetComponent<RectTransform>().transform.GetChild(i).GetComponent<DragDrop>().enabled = false;
                //continue;
            }
        }
    }
}
