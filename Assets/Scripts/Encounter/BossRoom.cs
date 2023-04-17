using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRoom : MonoBehaviour
{
    [SerializeField] GameObject player;
    public int count;
    public PlayerScoreTracker playerRef;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = player.GetComponent<PlayerScoreTracker>();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRef.killCount == count){
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            foreach (Chase chase_ai in FindObjectsOfType<Chase>())
                chase_ai.enabled = true;
        }
    }
}
