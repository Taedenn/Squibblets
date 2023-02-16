using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Encounter : MonoBehaviour
{
    [SerializeField] GameObject player;
    int correct_answer;
    [SerializeField] int random_range;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    List<GameObject> incorrect_buttons;
    GameObject correct_button;
    [SerializeField] GameObject question_text_box;
    List<GameObject> unactive_objects;
    [SerializeField] float deletion_delay = 2f;
    [SerializeField] AudioClip winSFX;
    [SerializeField] AudioClip loseSFX;
    AudioSource audio_player;
    ParticleSystem particles;
    SpriteRenderer enemy_renderer;
    bool isDead = false;
    [SerializeField] TextAsset question_file;
    string question;

    void Start() {
        question = GetRandomQuestion();
        correct_answer = GetCorrectAnswer(question);

        unactive_objects = new List<GameObject>{button1, button2, button3, question_text_box};

        incorrect_buttons = new List<GameObject>{button1, button2, button3};
        correct_button = incorrect_buttons[Random.Range(0, 3)];
        incorrect_buttons.Remove(correct_button);

        enemy_renderer = gameObject.GetComponent<SpriteRenderer>();
        particles = transform.GetComponentInChildren<ParticleSystem>();
        audio_player = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (isDead)
            return;

        foreach (GameObject obj in unactive_objects)
            obj.SetActive(true);

        SetButtonAnswers();

        correct_button.GetComponent<Button>().onClick.AddListener(Win);

        player.GetComponent<Movement>().enabled = false;
    }

    string GetRandomQuestion()
    {
        string[] questions_array = question_file.text.Split('\n');
        int random_index = Random.Range(0, questions_array.Length);
        return questions_array[random_index];
    }

    int GetCorrectAnswer(string question_text)
    {
        string[] question_parts = question_text.Split(' ');

        int first_number = int.Parse(question_parts[0]);
        char op = question_parts[1][0];
        int second_number = int.Parse(question_parts[2]);

        if (op == '+')
            return first_number + second_number;
        if (op == '-')
            return first_number - second_number;
        if (op == '*')
            return first_number * second_number;
        if (op == '/')
            return first_number / second_number;

        return -1;
    }

    void SetButtonAnswers()
    {
        List<int> nums_used = new List<int>();
        int random_number;

        foreach (GameObject buttonObject in incorrect_buttons) {
            random_number = Mathf.RoundToInt(Random.Range(-random_range, random_range));
            
            while (random_number == 0 || nums_used.Contains(random_number) || random_number + correct_answer < 0)
                random_number = Mathf.RoundToInt(Random.Range(-random_range, random_range));
            
            buttonObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText((correct_answer + random_number).ToString());
            buttonObject.transform.GetComponent<Button>().onClick.AddListener(() => WrongAnswerAction(buttonObject));
            nums_used.Add(random_number);
        }
        
        correct_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(correct_answer.ToString());
        question_text_box.GetComponent<TextMeshProUGUI>().SetText(question);
    }

    void WrongAnswerAction(GameObject button)
    {
        button.GetComponent<Image>().color = Color.red;
        audio_player.PlayOneShot(loseSFX);
    }

    void Win() 
    {
        isDead = true;

        foreach (GameObject obj in unactive_objects)
            obj.SetActive(false);

        player.GetComponent<Movement>().enabled = true;
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
