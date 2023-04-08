using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using PlayFab;


public class MainMenu : MonoBehaviour
{
    [Header ("Opening screen")]
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject logo;
    [SerializeField] GameObject spaceShip;
    [Header ("Menu selection")]
    [SerializeField] GameObject decisionPanel;
    [Header ("Level selection")]
    [SerializeField] GameObject levelSelectPanel;
    [SerializeField] TMP_Dropdown levelSelectDropdown;
    [Header ("User creation")]
    [SerializeField] GameObject LoginPanel;
    [SerializeField] GameObject usernamePanel;
    [Header ("Leaderboard")]
    [SerializeField] GameObject scoreSelectPanel;
    [SerializeField] TMP_Text failText;
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
        mainPanel.SetActive(false);
    }

    public void DecisionBackButton()
    {
        decisionPanel.SetActive(false);
        logo.SetActive(true);
        mainPanel.SetActive(true);
        failText.text = "";
    }

    public void LoginButton()
    {
        decisionPanel.SetActive(false);
        LoginPanel.SetActive(true);
        failText.text = "";
    }

    public void ScoresButton()
    {
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

    public void LevelsButton() 
    {
        mainPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }
    public void LevelSelectBackButton()
    {
        levelSelectPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void LevelSelectPlayButton()
    {
        int index = levelSelectDropdown.value;
        List<TMP_Dropdown.OptionData> options = levelSelectDropdown.options;

        SceneManager.LoadScene(options[index].text);
    }
}
