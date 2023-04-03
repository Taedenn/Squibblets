using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject statsButton;
    [SerializeField] GameObject logo;
    [SerializeField] GameObject decisionPanel;
    [SerializeField] GameObject studentLoginPanel;
    [SerializeField] GameObject teacherLoginPanel;
    [SerializeField] GameObject spaceShip;

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
        decisionPanel.SetActive(false);
        studentLoginPanel.SetActive(false);
        teacherLoginPanel.SetActive(false);
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
    }

    public void StudentButton()
    {
        spaceShip.SetActive(false);
        decisionPanel.SetActive(false);
        studentLoginPanel.SetActive(true);
    }

    public void TeacherButton()
    {
        spaceShip.SetActive(false);
        decisionPanel.SetActive(false);
        teacherLoginPanel.SetActive(true);
    }

    public void LoginBackButton()
    {
        studentLoginPanel.SetActive(false);
        teacherLoginPanel.SetActive(false);
        logo.SetActive(true);
        spaceShip.SetActive(true);
        playButton.SetActive(true);
        statsButton.SetActive(true);
    }
}
