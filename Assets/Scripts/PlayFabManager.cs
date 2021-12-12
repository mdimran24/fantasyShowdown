using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


public class PlayFabManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField emailInput;
    public Button RegisterButton;
    public Button LoginButton;

    void Start() {
        RegisterButton.onClick.AddListener(Register);
    }

    public void Register() {
        var request = new RegisterPlayFabUserRequest {
            Username = usernameInput.text,
            Password = passwordInput.text,
            Email = emailInput.text,
            RequireBothUsernameAndEmail = true
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result) {
        Debug.Log("Successfully registered!");
    }

    void OnError(PlayFabError error) {
        Debug.Log("Could not register the account.");
        Debug.Log(error.GenerateErrorReport());
    }

}
