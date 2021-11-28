using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Vector2 startPosition;
    private bool isDragging = false;
    private bool isOverDropZone = false;
    private GameObject chosenCardArea;
    private GameObject startParent;
    public GameObject Canvas;
    // Update is called once per frame

private void Awake() {
    Canvas = GameObject.Find("Main Canvas");
}

    void Update()
    {
        if (isDragging){
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
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
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {

        isDragging = false;
        if (isOverDropZone && chosenCardArea.transform.childCount <= 0)
        {
            transform.SetParent(chosenCardArea.transform, false);
        } else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }
}
