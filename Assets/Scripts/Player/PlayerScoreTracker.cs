using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreTracker : MonoBehaviour
{
    public int mistakes = 0;
    public int desiredKillCount = 1;
    public int killCount;
    [SerializeField] GameObject sceneLoader;
    
    public void AddMistake() {
        mistakes++;
    }
    public void AddKill() {
        killCount++;
        sceneLoader.GetComponent<SceneLoad>().CheckLevelCompletion(killCount, desiredKillCount, mistakes);
    }
}
