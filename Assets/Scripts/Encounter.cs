using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    bool in_encounter = false;
    GameObject player;
    Button correct_button;
    void OnTriggerEnter2D(Collider2D other) {
        GameObject canvas = transform.GetChild(0).gameObject;
        canvas.SetActive(true);
        correct_button = canvas.transform.GetChild(1).gameObject.GetComponent<Button>();
        correct_button.onClick.AddListener(Deletion);

        if(other.CompareTag("Player"))
        {
            player = other.gameObject;
            other.GetComponent<Movement>().enabled = false;
            in_encounter = true;
        }   
    }

    void Deletion() 
    {
        player.GetComponent<Movement>().enabled = true;
        Destroy(gameObject);
    }

}
