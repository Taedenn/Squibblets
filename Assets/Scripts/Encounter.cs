using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    GameObject player;
    Canvas canvas;
    Button correct_button;

    void Start() {
        canvas = transform.GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        correct_button = canvas.transform.Find("Correct_button").gameObject.GetComponent<Button>();
    }
    void OnTriggerEnter2D(Collider2D other) {
        Canvas canvas = transform.GetComponentInChildren<Canvas>();
        canvas.enabled = true;
        correct_button.onClick.AddListener(Deletion);

        if(other.CompareTag("Player"))
        {
            player = other.gameObject;
            other.GetComponent<Movement>().enabled = false;
        }   
    }

    void Deletion() 
    {
        player.GetComponent<Movement>().enabled = true;
        Destroy(gameObject);
    }

    Transform FindWithTag(Transform root, string tag)
    {
        foreach (Transform t in root.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag(tag)) return t;
        }
        return null;
    }

}
