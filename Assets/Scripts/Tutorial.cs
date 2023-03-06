
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector2 ref_position;

    public void Start(){
        ref_position = player.transform.position;
    }

    void Update()
    {
        Vector2 player_position = player.transform.position;
        if (player_position != ref_position)
        {
            Destroy(gameObject);
        }
    }
}