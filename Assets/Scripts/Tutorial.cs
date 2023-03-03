
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    private Vector2 moveInput;
    void Update()
    {
        if (moveInput != Vector2.zero)
        {
            Destroy(gameObject);
        }
    }
    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }
}