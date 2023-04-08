using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using PlayFab;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject statsButton;
    [SerializeField] GameObject logo;
    [SerializeField] GameObject decisionPanel;
    [SerializeField] TMP_Text failText;
    [SerializeField] GameObject LoginPanel;
    [SerializeField] GameObject usernamePanel;
    [SerializeField] GameObject scoreSelectPanel;
    [SerializeField] GameObject spaceShip;
    [Header ("Leaderboard")]
    [SerializeField] GameObject leaderboardPanel;
    [SerializeField] TMP_Text leaderboardHeaderText;
    [SerializeField] TMP_Dropdown leaderboardDropdown;

    public void StartGame()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Debug.Log("Quitted Good");
        Application.Quit();
    }

    public void StatsButton()
    {
        logo.SetActive(false);
        decisionPanel.SetActive(true);
        playButton.SetActive(false);
        statsButton.SetActive(false);
    }

    public void DecisionBackButton()
    {
        decisionPanel.SetActive(false);
        logo.SetActive(true);
        playButton.SetActive(true);
        statsButton.SetActive(true);
        failText.text = "";
    }

    public void LoginButton()
    {
        spaceShip.SetActive(false);
        decisionPanel.SetActive(false);
        LoginPanel.SetActive(true);
        failText.text = "";
    }

    public void ScoresButton()
    {
        spaceShip.SetActive(false);
        decisionPanel.SetActive(false);
        scoreSelectPanel.SetActive(true);
    }

    public void SelectLeaderboardButton()
    {
        Debug.Log("SOMET");
        scoreSelectPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
        leaderboardHeaderText.text = leaderboardDropdown.options[leaderboardDropdown.value].text;

        try {
            gameObject.GetComponent<PlayfabManager>().getLeaderboard();
        } 
        catch (PlayFabException) {
            leaderboardPanel.SetActive(false);
            decisionPanel.SetActive(true);
            spaceShip.SetActive(true);
            failText.text = "Must login first!";
        }
    }

    public void LoginBackButton()
    {
        LoginPanel.SetActive(false);
        decisionPanel.SetActive(true);
        spaceShip.SetActive(true);
    }

    public void SelectScoreBackButton()
    {
        scoreSelectPanel.SetActive(false);
        decisionPanel.SetActive(true);
        spaceShip.SetActive(true);
    }

    public void LeaderBoardBackButton()
    {
        leaderboardPanel.SetActive(false);
        scoreSelectPanel.SetActive(true);
    }
}
