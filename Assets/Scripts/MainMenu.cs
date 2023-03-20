using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*
    [SerializeField] GameObject button;

    void Start() 
    {
        button.transform.GetComponent<Button>().onClick.AddListener(StartTutorial); 
    }
    */
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
}
