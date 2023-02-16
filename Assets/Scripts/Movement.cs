using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float movement_speed = 8;
    [SerializeField] Sprite left_facing;
    [SerializeField] Sprite right_facing;
    [SerializeField] Sprite down_facing;
    [SerializeField] Sprite up_facing;
    SpriteRenderer player_renderer;

    void Start() 
    {
        player_renderer = GetComponent<SpriteRenderer>();    
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            player_renderer.sprite = right_facing;
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            player_renderer.sprite = left_facing;
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            player_renderer.sprite = up_facing;
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            player_renderer.sprite = down_facing;

        float x_movement = Input.GetAxis("Horizontal") * movement_speed * Time.deltaTime;
        float y_movement = Input.GetAxis("Vertical") * movement_speed * Time.deltaTime;
        transform.Translate(x_movement, 0, 0);
        transform.Translate(0, y_movement, 0);
    }
}
