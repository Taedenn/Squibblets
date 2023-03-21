using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoad : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject canvas;
    PlayerController playerRef;
    public Animator animator;
    private int nextScene;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = player.GetComponent<PlayerController>();
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRef.desiredKillCount == playerRef.killCount){
            nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            canvas.SetActive(true);
            animator.SetTrigger("FadeOut");

        }
    }
    public void OnFadeComplete(){
        SceneManager.LoadScene(nextScene);
    }
}
