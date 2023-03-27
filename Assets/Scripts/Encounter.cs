using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data;

public class Encounter : MonoBehaviour
{
    // Player stuff
    PlayerController playerRef;
    [SerializeField] GameObject player;
    // Question info
    [SerializeField] int random_range = 5;
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
    public enum difficulty_level
    {
        Easy,
        Medium,
        Hard,
        Very_Hard,
        Super_Hard,
        Boss_Fight
    };
    public difficulty_level difficulty = difficulty_level.Easy;
    


    void Start() {
        playerRef = player.GetComponent<PlayerController>();

        question = GetRandomQuestion();
        correct_answer = GetCorrectAnswer(question);

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

    string GetRandomQuestion()
    {
        switch (difficulty) {
            case (difficulty_level.Easy):
                return GetEasyQuestion();
            case (difficulty_level.Medium):
                return GetMediumQuestion();
            case (difficulty_level.Hard):
                return GetHardQuestion();
            case (difficulty_level.Very_Hard):
                return GetVeryHardQuestion();
            case (difficulty_level.Super_Hard):
                return GetSuperHardQuestion();
            case (difficulty_level.Boss_Fight):
                return GetBossFightQuestion();
            default:
                return "";
        }
    }
    string GetEasyQuestion() 
    {
        int first_number = Random.Range(0, 10);
        int second_number = Random.Range(0, 10);
        return $"{first_number} + {second_number}";
    }
    string GetMediumQuestion() 
    {
        int first_number = 0;
        int second_number = 1;

        while (first_number - second_number < 0) {
            first_number = Random.Range(0, 10);
            second_number = Random.Range(0, 10);
        }
        
        return $"{first_number} - {second_number}";
    }

    string GetHardQuestion()
    {
        int first_number = Random.Range(0, 10);
        int second_number = Random.Range(0, 10);
        int third_number = Random.Range(0, 10);

        char[] operands = {'+', '-'};
        char first_operand = operands[Random.Range(0, 2)];
        char second_operand = operands[Random.Range(0, 2)];

        string question = $"{first_number} {first_operand} {second_number} {second_operand} {third_number}";

        while (GetCorrectAnswer(question) < 0)
        {
            first_number = Random.Range(0, 10);
            second_number = Random.Range(0, 10);
            third_number = Random.Range(0, 10);

            first_operand = operands[Random.Range(0, 2)];
            second_operand = operands[Random.Range(0, 2)];

            question = $"{first_number} {first_operand} {second_number} {second_operand} {third_number}";
        }
        
        return question;
    }

    string GetVeryHardQuestion()
    {
        int first_number = Random.Range(0, 10);
        int second_number = Random.Range(10, 100);

        char[] operands = {'+', '-'};
        char operand = operands[Random.Range(0, 2)];

        int random_number = Random.Range(0, 2);

        string question = $"{second_number} {operand} {first_number}";

        while (GetCorrectAnswer(question) < 0 || GetCorrectAnswer(question) > 100) 
        {
            first_number = Random.Range(0, 10);
            second_number = Random.Range(10, 100);

            operand = operands[Random.Range(0, 2)];

            question = $"{second_number} {operand} {first_number}";
        }

        return question;
    }
    string GetSuperHardQuestion()
    {
        int first_number = Random.Range(10, 100);
        int second_number = Random.Range(10, 100);
        int third_number = Random.Range(10, 100);

        char[] operands = {'+', '-'};
        char first_operand = operands[Random.Range(0, 2)];
        char second_operand = operands[Random.Range(0, 2)];

        string question = $"{first_number} {first_operand} {second_number} {second_operand} {third_number}";

        while (GetCorrectAnswer(question) < 0 || GetCorrectAnswer(question) > 100) 
        {
            first_number = Random.Range(10, 100);
            second_number = Random.Range(10, 100);
            third_number = Random.Range(10, 100);

            first_operand = operands[Random.Range(0, 2)];
            second_operand = operands[Random.Range(0, 2)];

            question = $"{first_number} {first_operand} {second_number} {second_operand} {third_number}";
        }

        return question;
    }
    string GetBossFightQuestion()
    {
        int number = Random.Range(100, 999);
        int randomizer = Random.Range(0, 2);

        if (randomizer == 0)
        {
            return $"What number is in the one's place: {number}";
        }
        else if (random_range == 1)
        {
            return $"What number is in the ten's place: {number}";
        }
        else
        {
            return $"What number is in the hundredth's place: {number}";
        }
    }

    int GetCorrectAnswer(string question_text)
    {
        DataTable table = new DataTable();
        return System.Convert.ToInt32(table.Compute(question_text, null));
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

        playerRef.enabled = true;
        enemy_renderer.color = Color.red;

        KillCounter(playerRef);
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
