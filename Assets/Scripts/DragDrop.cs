using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Vector2 startPosition;
    private bool isDragging = false;
    private bool isOverDropZone = false;
    private GameObject chosenCardArea;
    // Update is called once per frame
    void Update()
    {
        if (isDragging){
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        chosenCardArea = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        chosenCardArea = null;
    }
    public void StartDrag()
    {
        startPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {

        isDragging = false;
        if (isOverDropZone)
        {
            transform.SetParent(chosenCardArea.transform, false);
        } else
        {
            transform.position = startPosition;
        }
    }
}
