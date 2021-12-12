using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


public class PlayFabRegister : MonoBehaviour
{
    public Text messageText;
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField emailInput;
    public Button RegisterButton;

    void Start() {
        RegisterButton.onClick.AddListener(Register);
    }

    public void Register() {
        
        if (usernameInput.text.Length < 1 || passwordInput.text.Length < 1 || emailInput.text.Length < 1) {
            messageText.text = "All fields must be filled.";
            return;
        } else if (passwordInput.text.Length < 6) {
            messageText.text = "Password too short, at least 6 characters needed.";
            return;
        }

        var request = new RegisterPlayFabUserRequest {
            Username = usernameInput.text,
            Password = passwordInput.text,
            Email = emailInput.text,
            RequireBothUsernameAndEmail = true
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result) {
        messageText.text = "Successfully registered!";
        Debug.Log("Successfully registered!");
    }

    void OnRegisterError(PlayFabError error) {
        messageText.text = "Could not register the account. Username or Email already taken.";
        Debug.Log("Could not register the account.");
        Debug.Log(error.GenerateErrorReport());
    }

}
