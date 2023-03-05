using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoad : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerController playerRef;
    public Animator animator;
    private int nextScene;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRef.desiredKillCount == playerRef.killCount){
            nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            animator.SetTrigger("FadeOut");

        }
    }
    public void OnFadeComplete(){
        SceneManager.LoadScene(nextScene);
    }
}
