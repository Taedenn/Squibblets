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

    [Header("Login UI")]
    public TMP_Text messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    [Header("Username")]
    public GameObject usernamePanel;
    public TMP_InputField nameInput;
    
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
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
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
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);

    }
    void OnLoginSuccess(LoginResult result) {
        messageText.text = "Logged in!";
        Debug.Log("Successful login/account created!");
        string name = null;
        
        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        if (name == null)
            usernamePanel.SetActive(true);

    }
    public void SubmitNameButton() {
        var request = new UpdateUserTitleDisplayNameRequest {
            DisplayName = nameInput.text
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result) {
        Debug.Log("Updated display name!");
        usernamePanel.SetActive(false);
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
    public void SendLeaderboard(int score) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate {
                    StatisticName = "Player scores",
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
            StatisticName = "Player scores",
            StartPosition = 0,
            MaxResultsCount = 30
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    void OnLeaderboardGet(GetLeaderboardResult result) {
        
        foreach(Transform item in rowsParent) {
            Destroy(item.gameObject);
        }

        foreach(var item in result.Leaderboard) {

            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.PlayFabId);
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}
