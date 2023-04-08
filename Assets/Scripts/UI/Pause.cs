using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject player;

    public void PauseButton()
    {
        player.GetComponent<PlayerController>().TerminateAnimations();
        player.GetComponent<PlayerController>().enabled = false;
        pausePanel.SetActive(true);
    }
    public void ResumeButton()
    {
        player.GetComponent<PlayerController>().enabled = true;
        pausePanel.SetActive(false);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
