using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    bool in_encounter = false;
    GameObject player;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            player = other.gameObject;
            other.GetComponent<Movement>().enabled = false;
            in_encounter = true;
        }   
    }

    void Update() {
        if (in_encounter)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                player.GetComponent<Movement>().enabled = true;
                Destroy(gameObject);

            }
        }    
    }

}
