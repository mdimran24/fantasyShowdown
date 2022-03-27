using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreScript : MonoBehaviour
{
    [SerializeField] private Window loreWindow;




    public void OpenLoreWindow()
    {
        loreWindow.gameObject.SetActive(true);
        loreWindow.exitButton.onClick.AddListener(ExitClicked);
        //loreWindow.cardName.text = name;
        // loreWindow.message.text = message;
    }
    public void ExitClicked()
    {
        Debug.Log("Exit Pressed");
        loreWindow.gameObject.SetActive(false);
    }





}
