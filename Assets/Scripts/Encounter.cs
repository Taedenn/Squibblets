using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Encounter : MonoBehaviour
{
    [SerializeField] string question;
    [SerializeField] int correct_answer;
    GameObject player;
    Canvas canvas;
    GameObject correct_button;
    GameObject incorrect_button;
    TextMeshProUGUI correct_text;
    TextMeshProUGUI incorrect_text;
    TextMeshProUGUI question_text;
    [SerializeField] float deletion_delay = 2f;
    [SerializeField] AudioClip winSFX;
    AudioSource audio_player;
    ParticleSystem particles;
    SpriteRenderer enemy_renderer;
    bool isDead = false;

    void Start() {
        canvas = FindFirstObjectByType<Canvas>();

        question_text = canvas.transform.Find("Question_text").GetComponent<TextMeshProUGUI>();
        question_text.enabled = false;

        correct_button = canvas.transform.Find("Correct_button").gameObject;
        incorrect_button = canvas.transform.Find("Incorrect_button").gameObject;
        
        correct_text = correct_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        incorrect_text = incorrect_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        correct_button.SetActive(false);
        incorrect_button.SetActive(false);

        enemy_renderer = gameObject.GetComponent<SpriteRenderer>();
        particles = transform.GetComponentInChildren<ParticleSystem>();
        audio_player = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (isDead)
            return;
        
        question_text.SetText(question);
        question_text.enabled = true;

        correct_text.SetText(correct_answer.ToString());
        correct_button.SetActive(true);
        incorrect_button.SetActive(true);        
        correct_button.GetComponent<Button>().onClick.AddListener(Win);

        player.GetComponent<Movement>().enabled = false;
    }

    void Win() 
    {
        isDead = true;
        player.GetComponent<Movement>().enabled = true;
        canvas.enabled = false;
        enemy_renderer.color = Color.red;

        audio_player.PlayOneShot(winSFX);
        particles.Play();
        Invoke("Deletion", deletion_delay);
    }

    void Deletion()
    {
        Destroy(gameObject);
    }

}
