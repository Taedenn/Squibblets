using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreTracker : MonoBehaviour
{
    int mistakes = 0;
    
    public void AddMistake() {
        mistakes++;
    }
}
