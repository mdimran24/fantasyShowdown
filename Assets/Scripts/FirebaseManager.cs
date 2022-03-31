using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using System.Linq;

public class FirebaseManager : MonoBehaviour
{
        public static FirebaseManager instance;

        public static string YourName;
        public static bool isLoggedIn;
        public int wins;
        public int losses;

        public static int wins;

        public static int losses;

        //Firebase variables
        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser User;
        public DatabaseReference DBref;

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

        //Database Variables
        [Header("UserData")]
        public TMP_Text usernameField;
        public TMP_Text winField;
        public TMP_Text lossField;
        public TMP_Text tiesField;
        public GameObject scoreElement;
        public Transform leaderboardContent;
        public bool hasCheckedLogin;

        //Ensures the autentication works when scenes are changed 
        private void Awake()
        {
                DontDestroyOnLoad(gameObject);
                if(instance == null)
                {
                        instance = this;
                }
                else if (instance != this)
                {
                        Destroy(instance.gameObject);
                        instance = this;
                }
        }

        private void Start()
        {
                StartCoroutine(CheckAndFixDependencies());
        }

        // Allows me to call variables and methods from other scripts
        private static FirebaseManager _singleton;
        public static FirebaseManager Singleton
        {
                get => _singleton;
                private set
                {
                        if (_singleton == null)
                        {
                                _singleton = value;
                        }
                        else if (_singleton != value)
                        {
                                Debug.Log($"{nameof(FirebaseManager)} instance already exists, destroying object!");
                                Destroy(value.gameObject);
                }
    }
}
        //checks to ensure all firebase dependencies are installed and throws an error if they are not
        private IEnumerator CheckAndFixDependencies()
        {
                var checkAndFixDependenciesTask = FirebaseApp.CheckAndFixDependenciesAsync();

                yield return new WaitUntil(predicate: () => checkAndFixDependenciesTask.IsCompleted);

                var dependencyResult = checkAndFixDependenciesTask.Result;

                if(dependencyResult == DependencyStatus.Available)
                {
                        IntializeFirebase();
                }
                else{
                        Debug.LogError($"Could not resolve all Firebase Dependencies: {dependencyResult}");
                }
        }

        // Starts up firebase
        private void IntializeFirebase()
        {
                Debug.Log("Setting up Authentication");
                auth = FirebaseAuth.DefaultInstance;

                StartCoroutine(CheckAutoLogin());

                auth.StateChanged += AuthStateChanged;
                AuthStateChanged(this, null);

                DBref = FirebaseDatabase.DefaultInstance.RootReference;
        }

        // checks if the player is logged in on loading up the game
        private IEnumerator CheckAutoLogin()
        {
                yield return new WaitForEndOfFrame();
                if(User != null)
                {
                        User = auth.CurrentUser;
                        var reloadUserTask = User.ReloadAsync();

                        yield return new WaitUntil(predicate: () => reloadUserTask.IsCompleted);

                }

        }       

        //checks if the authentication state has changed
        private void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
                if(auth.CurrentUser != User)
                {
                        bool singedIn = User != auth.CurrentUser && auth.CurrentUser != null;

                        if(!singedIn && User != null)
                        {
                                Debug.Log("Signed Out");
                                isLoggedIn = false;
                                YourName = "";
                        }

                        User = auth.CurrentUser;

                        if (singedIn)
                        {
                                Debug.Log($"Singed In: {User.DisplayName}");
                                YourName = User.DisplayName;
                                isLoggedIn = true;
                        }
                }
        }

        // clears the login fields
        public void ClearLoginFields(){
                emailLoginField.text = "";
                passwordLoginField.text = "";
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

        //Function for the signout button
        public void SignOutButton(){
                auth.SignOut();
                UIManager.instance.LoginScreen();
                ClearLoginFields();
                StartCoroutine(LoadUserData());
        }

        //Function for the Leaderboard Button
        public void LeaderboardButton()
        {
                StartCoroutine(LoadLeaderboardData());
        }

        //Function for the profile button
        public void ProfileButton()
        {
                if(User != null)
                {
                        StartCoroutine(LoadUserData());
                }
        }


        //Function that updates the auth data if a user is alogged in
        private void FixedUpdate()
    {
            if(auth != null && hasCheckedLogin == false)
            {
                    CheckAutoLogin();
                    hasCheckedLogin = true;
            }
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
                        StartCoroutine(LoadUserData());

                        yield return new WaitForSeconds(2);

                        usernameField.text = User.DisplayName;
                        UIManager.instance.UserDataScreen();
                        confirmLoginText.text = "";
                        ClearLoginFields();
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
                                                warningRegisterText.text = "Register Successful";
                                                StartCoroutine(UpdateUsernameAuth(_username));
                                                StartCoroutine(UpdateUsernameDatabase(_username));

                                                StartCoroutine(UpdateWins(0));
                                                StartCoroutine(UpdateLosses(0));
                                                StartCoroutine(UpdateTies(0));
                                        }
                                }
                        }

                }
        }

        private IEnumerator UpdateUsernameAuth(string _username)
  {
                //Create a user profile and set the username
                UserProfile profile = new UserProfile { DisplayName = _username };

                //Call the Firebase auth update user profile function passing the profile with the username
                var ProfileTask = User.UpdateUserProfileAsync(profile);
                //Wait until the task completes
                yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                if (ProfileTask.Exception != null)
                {
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                }
                else
                {
                        //Auth username is now updated
                }        
    }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
                //Set the currently logged in user username in the database
                var DBTask = DBref.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

                yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

                if (DBTask.Exception != null)
                {
                        Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
                }
                else
                {
                        //Database username is now updated
                }
    }

        //Function to manually update the number of wins a player has in the database
    public IEnumerator UpdateWins(int _wins) {
            var DBTask = DBref.Child("users").Child(User.UserId).Child("wins").SetValueAsync(_wins);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null){
                    Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else {

            }
    }

        //Function to manually update the number of losses a player has in the database
        public IEnumerator UpdateLosses(int _losses) {
            var DBTask = DBref.Child("users").Child(User.UserId).Child("losses").SetValueAsync(_losses);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null){
                    Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else {
                    
            }
    }

        //Function to manually update the number of wins a player has in the database
        public IEnumerator UpdateTies(int _ties) {
            var DBTask = DBref.Child("users").Child(User.UserId).Child("ties").SetValueAsync(_ties);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null){
                    Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else {
                    
            }
    }

        //Function to load the currently logged in users data from the database
    private IEnumerator LoadUserData () 
    {
            var DBTask = DBref.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            usernameField.text = "Not Logged in";
            winField.text = "0";
            lossField.text = "0";
           // tiesField.text = "0";
        }
        else
        {
            
            DataSnapshot snapshot = DBTask.Result;
            
            usernameField.text = snapshot.Child("username").Value.ToString();
            winField.text = snapshot.Child("wins").Value.ToString();
         //   wins =  (int) snapshot.Child("wins").Value;
            lossField.text = snapshot.Child("losses").Value.ToString();
          //  losses = (int) snapshot.Child("losses").Value;
            
           // tiesField.text = snapshot.Child("ties").Value.ToString();
        }
    }

    

        // Function to increase the number of wins in the database for the current user by 1
    public IEnumerator GetWins(){

            var DBTask = DBref.Child("users").Child(User.UserId).GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                    Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else
            {
                DataSnapshot snapshot = DBTask.Result;

                wins = int.Parse(snapshot.Child("wins").Value.ToString());

            }

           
    }

   // public static void AddWinsstatic(){
    //         StartCoroutine(AddWins());
   // }

        // Function to increase the number of losses in the database for the current user by 1
    public IEnumerator GetLosses(){

            var DBTask = DBref.Child("users").Child(User.UserId).GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                    Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else
            {
                DataSnapshot snapshot = DBTask.Result;

                losses = int.Parse(snapshot.Child("losses").Value.ToString());

            }

           
    }

        // Function to call all the data in the user database and store it into a game object
    private IEnumerator LoadLeaderboardData()
    {
            var DBTask = DBref.Child("users").OrderByChild("wins").GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else
            {
                DataSnapshot snapshot = DBTask.Result;
            
                foreach (Transform child in leaderboardContent.transform)
                {
                    Destroy(child.gameObject);
                }

                foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
                {
                    string username = childSnapshot.Child("username").Value.ToString();
                    int wins = int.Parse(childSnapshot.Child("wins").Value.ToString());
                    int losses = int.Parse(childSnapshot.Child("losses").Value.ToString());
                    int ties = int.Parse(childSnapshot.Child("ties").Value.ToString());
                
                    GameObject leaderboardElement = Instantiate(scoreElement, leaderboardContent);
                    leaderboardElement.GetComponent<ScoreElement>().NewScoreElement(username, wins, losses, ties);
                }

                UIManager.instance.LeaderboardScreen();
            }
    }
}