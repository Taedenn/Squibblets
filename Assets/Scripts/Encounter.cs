using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    GameObject player;
    Canvas canvas;
    Button correct_button;
    [SerializeField] float deletion_delay = 2f;
    [SerializeField] AudioClip winSFX;
    AudioSource audio_player;
    ParticleSystem particles;

    void Start() {
        canvas = transform.GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        correct_button = canvas.transform.Find("Correct_button").gameObject.GetComponent<Button>();

        particles = transform.GetComponentInChildren<ParticleSystem>();
        audio_player = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other) {
        canvas.enabled = true;
        correct_button.onClick.AddListener(Win);

        player.GetComponent<Movement>().enabled = false;
    }

    void Win() 
    {
        player.GetComponent<Movement>().enabled = true;
        canvas.enabled = false;

        audio_player.PlayOneShot(winSFX);
        particles.Play();
        Invoke("Deletion", deletion_delay);
    }

    void Deletion()
    {
        Destroy(gameObject);
    }

}
