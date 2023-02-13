using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasStart : MonoBehaviour
{
    [SerializeField] GameObject question_text;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    void Start()
    {
        question_text.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
    }
}
