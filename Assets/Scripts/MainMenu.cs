using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject button;

    void Start() 
    {
        button.transform.GetComponent<Button>().onClick.AddListener(StartTutorial); 
    }

    void StartTutorial()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
