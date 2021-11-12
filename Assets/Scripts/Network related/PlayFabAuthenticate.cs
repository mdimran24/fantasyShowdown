using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabAuthenticate : MonoBehaviour
{
    public InputField user;
    public InputField email;
    public InputField password;

    public bool isAuthenticated = false;

    LoginWithPlayFabRequest loginRequest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        loginRequest = new LoginWithPlayFabRequest();
        loginRequest.Username = user.text;
        loginRequest.Password = password.text;
        PlayFabClientAPI.LoginWithPlayFab(loginRequest, result => {
            isAuthenticated = true;
            Debug.Log("You are logged in");
        }, error => { isAuthenticated = false;
            Debug.Log(error.ErrorMessage);
        }, null);
    }

    public void Signup()
    {
        RegisterPlayFabUserRequest signupReq = new RegisterPlayFabUserRequest();
        signupReq.Email = email.text;
        signupReq.Username = user.text;
        signupReq.Password = password.text;
        PlayFabClientAPI.RegisterPlayFabUser(signupReq, result => {
            isAuthenticated = true;
            Debug.Log("You are signed up");
        }, error => {
            isAuthenticated = false;
            Debug.Log(error.ErrorMessage);
        }, null);
    }
}
