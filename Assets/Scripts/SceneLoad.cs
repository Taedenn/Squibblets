using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoad : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerController playerRef;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRef.desiredKillCount == playerRef.killCount){
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextScene);
        }
    }
}
