using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditsLoad : MonoBehaviour
{
    public Animator animator;
    private int nextScene;
    public float delay = 3;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            animator.SetTrigger("FadeOut");
        }
    }

    public void OnFadeComplete(){
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }
}
