using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float movement_speed = 8;
    SpriteRenderer player_renderer;

    void Start() 
    {
        player_renderer = GetComponent<SpriteRenderer>();    
    }

    void Update()
    {
        // if(Input.GetKey(KeyCode.RightArrow))
        // {
        //     player_renderer.
        // }
        // if(Input.GetKey(KeyCode.LeftArrow))
        // {
        //     player_renderer.
        // }

        float x_movement = Input.GetAxis("Horizontal") * movement_speed * Time.deltaTime;
        float y_movement = Input.GetAxis("Vertical") * movement_speed * Time.deltaTime;
        transform.Translate(x_movement, 0, 0);
        transform.Translate(0, y_movement, 0);
    }
}
