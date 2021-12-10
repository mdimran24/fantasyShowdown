using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class AccountManager : MonoBehaviour {

    public static AccountManager Instance;

    void Awake() {
        Instance = this;
    }

    public void CreateAccount (string username, string password, string email) {
        PlayFabClientAPI.RegisterPlayFabUser(
            new RegisterPlayFabUserRequest() {
                Email = email,
                Password = password,
                Username = username,
                RequireBothUsernameAndEmail = true
            },
            response => {
                Debug.Log($"Successfully registered the user: {username}");
            },
            error => {
                Debug.Log($"Could not register the account. \n {error.ErrorMessage}");
            }
        );
    }

}
