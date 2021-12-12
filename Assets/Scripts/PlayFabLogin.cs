using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


public class PlayFabLogin : MonoBehaviour
{
    public Text messageText;
    public InputField usernameInput;
    public InputField passwordInput;
    public Button LoginButton;

    void Start() {
        LoginButton.onClick.AddListener(Login);
    }

    public void Login() {

        if (usernameInput.text.Length < 1 || passwordInput.text.Length < 1) {
            messageText.text = "All fields must be filled.";
            return;
        }

        var loginRequest = new LoginWithPlayFabRequest() {
            Username = usernameInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithPlayFab(loginRequest, OnLoginSuccess, OnLoginError);
    }

    void OnLoginSuccess(LoginResult result) {
        messageText.text = "You are now logged in!";
        Debug.Log("You are now logged in!");
    }

    void OnLoginError(PlayFabError error) {
        messageText.text = "Could not login. Username or password incorrect.";
        Debug.Log("Could not login.");
        Debug.Log(error.GenerateErrorReport());
    }

}