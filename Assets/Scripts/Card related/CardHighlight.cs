using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHighlight : MonoBehaviour
{
  public GameObject Canvas;
  private GameObject selCard;

  private void Awake() {
      Canvas = GameObject.Find("Main Canvas");

  }

  public void OnHoverEnter(){
      selCard = Instantiate(gameObject, new Vector2(Input.mousePosition.x, Input.mousePosition.y + 250), Quaternion.identity);
      selCard.transform.SetParent(Canvas.transform, false);
      selCard.layer = LayerMask.NameToLayer("Zoom");

      RectTransform rect = selCard.GetComponent<RectTransform>();
      rect.sizeDelta = new Vector2(250, 400);
  }

public void OnHoverExit(){
Destroy(selCard);
}
}
