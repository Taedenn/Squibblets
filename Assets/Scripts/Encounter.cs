using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Encounter : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] int random_range = 5;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    [SerializeField] GameObject question_text_box;
    [SerializeField] float deletion_delay = 2f;
    [SerializeField] AudioClip winSFX;
    [SerializeField] AudioClip loseSFX;
    [SerializeField] AudioClip button_selectSFX;
    [SerializeField] TextAsset question_file;

    List<GameObject> incorrect_buttons;
    GameObject correct_button;
    List<GameObject> unactive_objects;
    AudioSource audio_player;
    ParticleSystem particles;
    SpriteRenderer enemy_renderer;
    bool isDead = false;
    string question;
    int correct_answer;
    GameObject selected_button;
    bool inEncounter = false;
    float last_button_change = 0;
    float last_button_select = 0;
    private Color originalColor;

    public PlayerController playerRef;

    void Start() {
        question = GetRandomQuestion();
        correct_answer = GetCorrectAnswer(question);

        unactive_objects = new List<GameObject>{button1, button2, button3, question_text_box};
        SetupButtons();

        enemy_renderer = gameObject.GetComponent<SpriteRenderer>();
        particles = transform.GetComponentInChildren<ParticleSystem>();
        audio_player = GetComponent<AudioSource>();
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
    }

    void CheckButtonChange()
    {
        if (Time.time - last_button_change < 0.1f)
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
        if (Time.time - last_button_select < 0.1f)
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

    string GetRandomQuestion()
    {
        string[] questions_array = question_file.text.Split('\n');
        int random_index = Random.Range(0, questions_array.Length);
        return questions_array[random_index];
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

        KillCounter(player.GetComponent<PlayerController>());
        audio_player.PlayOneShot(winSFX);
        particles.Play();
        ResetButtons(originalColor);
        Invoke("Deletion", deletion_delay);
    }

    void Deletion()
    {
        Destroy(gameObject);
    }

    void KillCounter(PlayerController playerRef){
        playerRef.killCount++;
    }
}
