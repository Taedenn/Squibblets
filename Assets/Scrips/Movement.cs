using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float movement_speed = 15;

    void Update()
    {
        // transform.Translate(0, movement_speed, 0);
        float x_movement = Input.GetAxis("Horizontal") * movement_speed * Time.deltaTime;
        float y_movement = Input.GetAxis("Vertical") * movement_speed * Time.deltaTime;
        transform.Translate(x_movement, 0, 0);
        transform.Translate(0, y_movement, 0);
    }
}
