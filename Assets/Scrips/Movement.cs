using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float movement_speed = 0.01f;

    void Update()
    {
        transform.Translate(0, movement_speed, 0);
    }
}
