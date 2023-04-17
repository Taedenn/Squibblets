using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data;

public class Enemy : MonoBehaviour
{
    // Player stuff
    [SerializeField] GameObject player;
    // Question info
    int random_range;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    [SerializeField] GameObject question_text_box;
    GameObject correct_button;
    GameObject selected_button;
    List<GameObject> incorrect_buttons;
    List<GameObject> unactive_objects;
    string question;
    int correct_answer;
    // Winning and audio
    [SerializeField] float deletion_delay = 2f;
    [SerializeField] AudioClip winSFX;
    [SerializeField] AudioClip loseSFX;
    [SerializeField] AudioClip button_selectSFX;
    [SerializeField] AudioSource audio_player;
    // Renderer stuff
    ParticleSystem particles;
    SpriteRenderer enemy_renderer;
    bool isDead = false;
    // Encounter info
    bool inEncounter = false;
    float last_button_change = 0;
    float last_button_select = 0;
    private Color originalColor;
    // Difficulty stuff
    public QuestionSetup.difficulty_level difficulty = QuestionSetup.difficulty_level.Easy;

    void Start() {
        question = QuestionSetup.GetRandomQuestion(difficulty);
        correct_answer = QuestionSetup.GetCorrectAnswer(question, difficulty);
        random_range = QuestionSetup.GetRandomRange(difficulty);

        unactive_objects = new List<GameObject>{button1, button2, button3, question_text_box};
        SetupButtons();

        enemy_renderer = gameObject.GetComponent<SpriteRenderer>();
        particles = transform.GetComponentInChildren<ParticleSystem>();
    }

    void Update() 
    {
        if (!inEncounter)
            return;
        
        CheckButtonChange();
        CheckButtonSelection();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (isDead)
            return;

        inEncounter = true;

        foreach (GameObject obj in unactive_objects)
            obj.SetActive(true);
        originalColor = button1.GetComponent<Image>().color;

        SetButtonAnswers();

        correct_button.GetComponent<Button>().onClick.AddListener(Win);

        player.GetComponent<PlayerController>().TerminateAnimations();
        player.GetComponent<PlayerController>().enabled = false;

        foreach (Chase chase_ai in FindObjectsOfType<Chase>())
            chase_ai.enabled = false;
    }

    void CheckButtonChange()
    {
        if (Time.time - last_button_change < 0.4f)
            return;

        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && selected_button == button1)
            ChangeButton(button2);
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && selected_button == button2)
            ChangeButton(button3);
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && selected_button == button3)
            ChangeButton(button2);
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && selected_button == button2)
            ChangeButton(button1);
    }

    void CheckButtonSelection()
    {
        if (Time.time - last_button_select < 0.4f)
            return;

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return)) && selected_button == correct_button){
            Win();
        }
        else if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return)) && selected_button != correct_button){
            WrongAnswerAction(selected_button);
            last_button_select = Time.time;
        }
    }

    void ChangeButton(GameObject button)
    {
        selected_button.transform.Find("Border").GetComponent<SpriteRenderer>().enabled = false;
        button.transform.Find("Border").GetComponent<SpriteRenderer>().enabled = true;
        audio_player.PlayOneShot(button_selectSFX);
        selected_button = button;
        last_button_change = Time.time;
    }

    void SetupButtons()
    {
        incorrect_buttons = new List<GameObject>{button1, button2, button3};
        correct_button = incorrect_buttons[Random.Range(0, 3)];
        incorrect_buttons.Remove(correct_button);

        selected_button = button2;
        button1.transform.Find("Border").GetComponent<SpriteRenderer>().enabled = false;
        button3.transform.Find("Border").GetComponent<SpriteRenderer>().enabled = false;        
    }

    void SetButtonAnswers()
    {
        question_text_box.GetComponent<TextMeshProUGUI>().SetText(question);
        correct_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(correct_answer.ToString());

        if (difficulty == QuestionSetup.difficulty_level.Boss_Fight)
        {            
            GameObject button1 = incorrect_buttons[0];
            GameObject button2 = incorrect_buttons[1];

            string[] string_array = question.Split();
            string number = string_array[string_array.Length - 1];

            if (question.Contains("hundred")) {
                button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText((number[1]).ToString());
                button1.transform.GetComponent<Button>().onClick.AddListener(() => WrongAnswerAction(button1));
                button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText((number[2]).ToString());
                button2.transform.GetComponent<Button>().onClick.AddListener(() => WrongAnswerAction(button1));
            }
            else if (question.Contains("ten"))
            {
                button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText((number[0]).ToString());
                button1.transform.GetComponent<Button>().onClick.AddListener(() => WrongAnswerAction(button1));
                button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText((number[2]).ToString());
                button2.transform.GetComponent<Button>().onClick.AddListener(() => WrongAnswerAction(button1));
            }
            else
            {
                button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText((number[0]).ToString());
                button1.transform.GetComponent<Button>().onClick.AddListener(() => WrongAnswerAction(button1));
                button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText((number[1]).ToString());
                button2.transform.GetComponent<Button>().onClick.AddListener(() => WrongAnswerAction(button1));
            }
            return;
        }

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
    }

    void WrongAnswerAction(GameObject button)
    {
        button.GetComponent<Image>().color = Color.red;
        audio_player.PlayOneShot(loseSFX);
        player.GetComponent<PlayerScoreTracker>().AddMistake();
    }
    void ResetButtons(Color originalColor){
        button1.GetComponent<Image>().color = originalColor;
        button2.GetComponent<Image>().color = originalColor;
        button3.GetComponent<Image>().color = originalColor;
        SetupButtons();
    }

    void Win() 
    {
        isDead = true;
        inEncounter = false;

        foreach (GameObject obj in unactive_objects)
            obj.SetActive(false);

        player.GetComponent<PlayerController>().enabled = true;
        enemy_renderer.color = Color.red;

        player.GetComponent<PlayerScoreTracker>().AddKill();
        audio_player.PlayOneShot(winSFX);
        particles.Play();
        ResetButtons(originalColor);
        Invoke("Deletion", deletion_delay);

        foreach (Chase chase_ai in FindObjectsOfType<Chase>())
            chase_ai.enabled = false;
    }

    void Deletion()
    {
        Destroy(gameObject);
    }

}
