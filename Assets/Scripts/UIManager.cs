using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    
    public GameObject loginUI;
    public GameObject userDataUI;
    public GameObject leaderboardUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ClearScreen() {
        loginUI.SetActive(false);
        userDataUI.SetActive(false);
        leaderboardUI.SetActive(false);
    }

    
    public void LoginScreen() 
    {
        ClearScreen();
        loginUI.SetActive(true);

    }

    public void UserDataScreen()
    {
        ClearScreen();
        userDataUI.SetActive(true);
    }

    public void LeaderboardScreen()
    {
        ClearScreen();
        leaderboardUI.SetActive(true);
    }
}
