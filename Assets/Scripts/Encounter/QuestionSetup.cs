using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class QuestionSetup : MonoBehaviour
{
    public enum difficulty_level
    {
        Easy,
        Medium,
        Hard,
        Very_Hard,
        Super_Hard,
        Boss_Fight
    };

    public static string GetRandomQuestion(difficulty_level difficulty)
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
                return GetDigitQuestion();
            default:
                return "";
        }
    }
    public static string GetEasyQuestion() 
    {
        int first_number = Random.Range(0, 10);
        int second_number = Random.Range(0, 10);
        return $"{first_number} + {second_number}";
    }
    public static string GetMediumQuestion() 
    {
        int first_number = 0;
        int second_number = 1;

        while (first_number - second_number < 0) {
            first_number = Random.Range(0, 10);
            second_number = Random.Range(0, 10);
        }
        
        return $"{first_number} - {second_number}";
    }

    public static string GetHardQuestion()
    {
        int first_number = Random.Range(0, 10);
        int second_number = Random.Range(0, 10);
        int third_number = Random.Range(0, 10);

        char[] operands = {'+', '-'};
        char first_operand = operands[Random.Range(0, 2)];
        char second_operand = operands[Random.Range(0, 2)];

        string question = $"{first_number} {first_operand} {second_number} {second_operand} {third_number}";

        while (GetCorrectAnswer(question, difficulty_level.Hard) < 0)
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

    public static string GetVeryHardQuestion()
    {
        int first_number = Random.Range(0, 10);
        int second_number = Random.Range(10, 100);

        char[] operands = {'+', '-'};
        char operand = operands[Random.Range(0, 2)];

        int random_number = Random.Range(0, 2);

        string question = $"{second_number} {operand} {first_number}";

        while (GetCorrectAnswer(question, difficulty_level.Very_Hard) < 0 || GetCorrectAnswer(question, difficulty_level.Very_Hard) > 100) 
        {
            first_number = Random.Range(0, 10);
            second_number = Random.Range(10, 100);

            operand = operands[Random.Range(0, 2)];

            question = $"{second_number} {operand} {first_number}";
        }

        return question;
    }
    public static string GetSuperHardQuestion()
    {
        int first_number = Random.Range(10, 100);
        int second_number = Random.Range(10, 100);
        int third_number = Random.Range(10, 100);

        char[] operands = {'+', '-'};
        char first_operand = operands[Random.Range(0, 2)];
        char second_operand = operands[Random.Range(0, 2)];

        string question = $"{first_number} {first_operand} {second_number} {second_operand} {third_number}";

        while (GetCorrectAnswer(question, difficulty_level.Super_Hard) < 0 || GetCorrectAnswer(question, difficulty_level.Super_Hard) > 100) 
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
    public static string GetDigitQuestion()
    {
        int number = Random.Range(100, 999);

        // Ensure that no digit repeats
        while (number.ToString().Length != new HashSet<char>(number.ToString()).Count)
        {
            number = Random.Range(100, 999);
        }

        int digit_place = Random.Range(0, 3);

        if (digit_place == 0)
        {
            return $"What is in the hundred's place: {number}";
        }
        else if (digit_place == 1)
        {
            return $"What is in the ten's place: {number}";
        }
        else
        {
            return $"What is in the one's place: {number}";
        }
    }
    public static int GetCorrectAnswer(string question_text, difficulty_level difficulty)
    {
        if (difficulty == difficulty_level.Boss_Fight)
        {
            string[] string_array = question_text.Split();
            string number = string_array[string_array.Length - 1];

            if (question_text.Contains("hundred"))
            {
                char number_char = number[0];
                return (int)char.GetNumericValue(number_char);
            }
            else if (question_text.Contains("ten"))
            {
                char number_char = number[1];
                return (int)char.GetNumericValue(number_char);
            }
            else
            {
                char number_char = number[2];
                return (int)char.GetNumericValue(number_char);
            }
        }

        DataTable table = new DataTable();
        return System.Convert.ToInt32(table.Compute(question_text, null));
    }
    public static int GetRandomRange(difficulty_level difficulty)
    {
        switch(difficulty) 
        {
            case QuestionSetup.difficulty_level.Medium:
                return 5;
            case QuestionSetup.difficulty_level.Hard:
                return 10;
            case QuestionSetup.difficulty_level.Very_Hard:
                return 15;
            case QuestionSetup.difficulty_level.Super_Hard:
                return 20;
            default:
                return 5;
        }
    }
}
