using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowsParent;

    [Header("UI")]
    public TMP_Text messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    
    public void registerButton() {
        var request = new RegisterPlayFabUserRequest {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    public void loginButton() {
        var request = new LoginWithEmailAddressRequest {
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnSuccess, OnError);
    }

    public void resetPasswordButton() {
        var request = new SendAccountRecoveryEmailRequest{
            Email = emailInput.text,
            TitleId = "97ABD"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void Start()
    {
    }

    void login() {
        var request = new LoginWithCustomIDRequest() {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);

    }
    void OnSuccess(LoginResult result) {
        messageText.text = "Logged in!";
        Debug.Log("Successful login/account created!");
    }
    void OnError(PlayFabError error) {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result) {
        Debug.Log("Something happened");
        messageText.text = "Registered and logged in!";
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result) {
        messageText.text = "Password reset email sent";
    }
    public void SendLeaderoard(int score) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate {
                    StatisticName = "Player score",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) {
        Debug.Log("Successful leaderboard sent!");
    }
    public void getLeaderboard() {
        var request = new GetLeaderboardRequest {
            StatisticName = "Player score",
            StartPosition = 0,
            MaxResultsCount = 30
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    void OnLeaderboardGet(GetLeaderboardResult result) {
        

        foreach(var item in result.Leaderboard) {

            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TextMeshPro[] texts = newGo.GetComponentsInChildren<TextMeshPro>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.PlayFabId;
            texts[0].text = item.StatValue.ToString();

            Debug.Log(item.PlayFabId);
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}
