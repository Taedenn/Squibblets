using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;


public class SceneLoad : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    public Animator animator;
    private int nextScene;
    void Start()
    {
        canvas.SetActive(false);
    }

    public void CheckLevelCompletion(int killCount, int desiredKillCount, int mistakes)
    {
        if(desiredKillCount == killCount){
            nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            canvas.SetActive(true);
            animator.SetTrigger("FadeOut");

            try {
                gameObject.GetComponent<PlayfabManager>().SendLeaderboard(mistakes);
            }
            catch(PlayFabException) {}
        }
    }
    public void OnFadeComplete(){
        SceneManager.LoadScene(nextScene);
    }
}
