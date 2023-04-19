using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] AudioClip bossFightSFX;
    public int count;
    PlayerScoreTracker playerRef;
    bool inBossFight = false;
    void Start()
    {
        playerRef = player.GetComponent<PlayerScoreTracker>();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        foreach (Chase chase_ai in FindObjectsOfType<Chase>())
                chase_ai.enabled = false;
    }

    void Update()
    {
        if(playerRef.killCount == count) {
            audioPlayer.clip = bossFightSFX;
            audioPlayer.Play();

            inBossFight = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            foreach (Chase chase_ai in FindObjectsOfType<Chase>())
                chase_ai.enabled = true;
            
            // Disable this script so that enemies don't have their chaseAI re-enabled every frame
            // even after we've disabled it in Enemy.OnTriggerEnter2D
            GetComponent<MonoBehaviour>().enabled = false;
        }
        
    }
}
