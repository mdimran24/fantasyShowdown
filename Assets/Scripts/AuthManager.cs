using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;

public class AuthManager : MonoBehaviour
{

        //Firebase variables
        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser User;

        //Login Variables
        [Header("Login")]
        public TMP_InputField emailLoginField;
        public TMP_InputField passwordLoginField;
        public TMP_Text warningLoginText;
        public TMP_Text confirmLoginText;

        //Register Variables
        [Header("Register")]
        public TMP_InputField usernameRegisterField;
        public TMP_InputField emailRegisterField;
        public TMP_InputField passwordRegisterField;
        public TMP_InputField passwordRegisterVerifyField;
        public TMP_Text warningRegisterText;

        private void Awake()
        {
                FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
                {
                        dependencyStatus = task.Result;
                        if (dependencyStatus == DependencyStatus.Available)
                        {
                                IntializeFirebase();
                        }
                        else
                        {
                                Debug.LogError("Couldn't resolve Firebase dependencies:" + dependencyStatus);
                        }
                });
        }

        // Starts up firebase
        private void IntializeFirebase()
        {
                Debug.Log("Setting up Authentication");
                auth = FirebaseAuth.DefaultInstance;
        }

        // Function for login button
        public void LoginButton()
        {
                StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
        }

        // Function for register button
        public void RegisterButton()
        {
                StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
        }

        // Handles the login authentication
        private IEnumerator Login(string _email, string _password)
        {
                var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);

                yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

                if (LoginTask.Exception != null)
                {
                        Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
                        FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                        string loginFail = "Login Failed";
                        switch (errorCode)
                        {
                                case AuthError.MissingEmail:
                                        loginFail = "No Email";
                                        break;
                                case AuthError.MissingPassword:
                                        loginFail = "No Password";
                                        break;
                                case AuthError.WrongPassword:
                                        loginFail = "Incorrect Email/Password";
                                        break;
                                case AuthError.InvalidEmail:
                                        loginFail = "Incorrect Email/Password";
                                        break;
                                case AuthError.UserNotFound:
                                        loginFail = "Incorrect Email/Password";
                                        break;
                        }
                        warningLoginText.text = loginFail;
                }
                else
                {
                        User = LoginTask.Result;
                        Debug.LogFormat("User successfully logged in: {0} ({1})", User.DisplayName, User.Email);
                        warningLoginText.text = "";
                        confirmLoginText.text = "Logged In";
                }
        }

        //Handles the registration authentication
        private IEnumerator Register(string _email, string _password, string _username)
        {
                if (_username == "")
                {
                        warningRegisterText.text = "No Username";
                }
                else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
                {
                        warningRegisterText.text = "Passwords Do Not Match";
                }
                else
                {
                        var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

                        yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

                        if (RegisterTask.Exception != null)
                        {
                                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                                string registerFail = "Register Failed";
                                switch (errorCode)
                                {
                                        case AuthError.MissingEmail:
                                                registerFail = "No Email";
                                                break;
                                        case AuthError.MissingPassword:
                                                registerFail = "No Password";
                                                break;
                                        case AuthError.WeakPassword:
                                                registerFail = "Weak Password";
                                                break;
                                        case AuthError.EmailAlreadyInUse:
                                                registerFail = "Email Already In Use";
                                                break;                                        
                                }
                                warningRegisterText.text = registerFail;
                        }
                        else
                        {
                                User = RegisterTask.Result;

                                if(User != null)
                                {
                                        UserProfile profile = new UserProfile {DisplayName = _username};

                                        var ProfileTask = User.UpdateUserProfileAsync(profile);

                                        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                                        if (ProfileTask.Exception != null)
                                        {
                                                Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                                                FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                                                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                                                warningRegisterText.text = "Username Failed to Set";
                                        }
                                        else
                                        {
                                                UIManager.instance.LoginScreen();
                                                warningRegisterText.text = "";
                                        }
                                }
                        }

                }
        }

}
