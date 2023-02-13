using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasStart : MonoBehaviour
{
    [SerializeField] GameObject question_text;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    void Start()
    {
        GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
        question_text.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
    }
}
