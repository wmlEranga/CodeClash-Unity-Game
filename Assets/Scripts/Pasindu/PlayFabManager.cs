/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;


public class PlayFabManager : MonoBehaviour
{
    [Header("UI")]
    public Text messageText;
    public TMP_InputField userNameInput;
    public TMP_InputField emailInput; 
    public TMP_InputField passwordInput;

    public TMP_InputField emailInputLog;
    public TMP_InputField passwordInputLog;

    public TMP_InputField emailInputForget;

    public GameObject mainMenu;
    public GameObject Register;
    public GameObject LoginB;
    public GameObject Leaderboard;

    public Text messageTextName;
    public Text messageTextScore;


    public String name = "";
    //public Text nameText;

    public bool flagLog = false;

// Register/Login/ResetPassword (Episode 6)
    public void RegisterButton() {

        if (passwordInput.text.Length < 6) {
            messageText.text = "Password too short..!";
            return;
        }
        name = userNameInput.text;

        var request = new RegisterPlayFabUserRequest{
            Username = userNameInput.text,  
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail= false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result) {
        mainMenu.SetActive(true);
        Register.SetActive(false);
        messageText.text = "Registered and logged in!"; 
    }

    public void LoginButon()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInputLog.text,
            Password = passwordInputLog.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in !";
        Debug.Log("Successful login");

        flagLog = true;

        mainMenu.SetActive(true);
        LoginB.SetActive(false);
        //GetCharacters();
    }


    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInputForget.text,
            TitleId = "16E0D",
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Password reset mail sent";
    }


    public void LeaderboardButton()
    {
        Leaderboard.SetActive(true);
        mainMenu.SetActive(false);
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            GetPlayerUsername();
        }
        else
        {
            messageText.text = "You must be logged in to view the leaderboard.";
        }
        messageTextScore.text = "rrrrr";
  
    }

    void GetPlayerUsername()
    {
        var request = new GetAccountInfoRequest();

        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnError);
    }

    void OnGetAccountInfoSuccess(GetAccountInfoResult result)
    {
        // Retrieve the username from the result and update the nameText component
        string username = result.AccountInfo.Username;
        messageTextName.text = username;
    }


    void Start()
    {
        //Login();
    }



/*
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);

    }


    void OnSuccess(LoginResult result)
    {
        Debug.Log("Sucessful login");
    }
*/


   // void OnError(PlayFabError error)
   // {
   //     messageText.text = error.ErrorMessage;
    //    Debug.Log("Error");
    //    Debug.Log(error.GenerateErrorReport());
//
   // }

    /*
    
    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "PlatformScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);

    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        StringBuilder leaderboardBuilder = new StringBuilder();
        foreach (var item in result.Leaderboard)
        {
            Debug.Log(item.Position + " " + item.PlayFabId + " " +item.StatValue);
        }
        leaderboardText.text = leaderboardBuilder.ToString();
    }
    */
    
//}

