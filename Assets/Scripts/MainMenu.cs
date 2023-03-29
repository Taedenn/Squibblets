using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button statsButton;
    [SerializeField] GameObject logo;
    [SerializeField] GameObject DecisionPanel;

    /*
    [SerializeField] GameObject button;
    private int nextScene;

    void Start() 
    {
        button.transform.GetComponent<Button>().onClick.AddListener(StartTutorial); 
    }
    */
    void Start()
    {
        DecisionPanel.SetActive(false);
    }
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
        DecisionPanel.SetActive(true);
        playButton.interactable = false;
        statsButton.interactable = false;
    }

    public void BackButton()
    {
        DecisionPanel.SetActive(false);
        logo.SetActive(true);
        playButton.interactable = true;
        statsButton.interactable = true;
    }
}
