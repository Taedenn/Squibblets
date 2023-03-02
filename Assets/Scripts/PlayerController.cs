using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
// NOTE: The movement for this script uses the new InputSystem. The player needs to have a PlayerInput
// component added and the Behaviour should be set to Send Messages so that the OnMove and OnFire methods
// actually trigger
 
public class PlayerController : MonoBehaviour
{
    //buttons
    [SerializeField] GameObject right;
    [SerializeField] GameObject left;
    [SerializeField] GameObject up;
    [SerializeField] GameObject down;
    //player assets
    [SerializeField] GameObject playerLeft;
    [SerializeField] GameObject playerRight;
    [SerializeField] GameObject playerBack;
    SpriteRenderer player;
    //for animations
    Animator animator;

    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    private Vector2 moveInput;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Rigidbody2D rb;


    public void Start()
    {
        player = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
 
    public void FixedUpdate()
    {
        // Try to move player in input direction, followed by left right and up down input if failed
        if(moveInput != Vector2.zero){
            // setting bool variable to result of method MovePlayer,
            // MovePlayer looks for collisions ahead of desired input movement.
            // If there is a collision, it will return false.
            bool success = MovePlayer(moveInput);
            if(!success)
            {
                // try left/right movement after hitting collider
                // this checks if we can move horizontally
                success = MovePlayer(new Vector2(moveInput.x, 0));

                if(!success)
                    {
                        // then this will check if vertical movement is possible
                        success = MovePlayer(new Vector2(0, moveInput.y));
                    }
            }
            // setting animations based on movement
            else if(moveInput.x > 0){
                    animator.SetBool("walk_left", false);
                    animator.SetBool("walk_front", false);
                    animator.SetBool("walk_back", false);
                    animator.SetBool("walk_right", success);
            }
            else if(moveInput.x < 0){
                    animator.SetBool("walk_right", false);
                    animator.SetBool("walk_front", false);
                    animator.SetBool("walk_back", false);
                    animator.SetBool("walk_left", success);
            }
            else if(moveInput.y < 0){
                    animator.SetBool("walk_right", false);
                    animator.SetBool("walk_left", false);
                    animator.SetBool("walk_back", false);
                    animator.SetBool("walk_front", success);
            }
            else if(moveInput.y > 0){
                    animator.SetBool("walk_right", false);
                    animator.SetBool("walk_left", false);
                    animator.SetBool("walk_front", false);
                    animator.SetBool("walk_back", success);
            }
        }
        else {
            animator.SetBool("walk_right", false);
            animator.SetBool("walk_left", false);
            animator.SetBool("walk_front", false);
            animator.SetBool("walk_back", false);
        }
 
    }
 
    // Tries to move the player in a direction by casting in that direction by the amount
    // moved plus an offset. If no collisions are found, it moves the player
    // Returns true or false depending on if a move was executed
    public bool MovePlayer(Vector2 direction)
    {
        // Check for potential collisions
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset
 
        if (count == 0)
        {
            Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;
            // No collisions
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    //Following methods set up moveInput for buttons on screen

    //Up button is pressed down
    public void UpIsDown(){
        moveInput = new Vector2(0,1);
    }

    //Up button is not pressed down
    public void UpIsUp(){
        moveInput = new Vector2(0,0);
    }

    //Down button is pressed down
    public void DownIsDown(){
        moveInput = new Vector2(0,-1);
    }

    //Down button is not pressed down
    public void DownIsUp(){
        moveInput = new Vector2(0,0);
    }

    //Right button is pressed down
    public void RightIsDown(){
        moveInput = new Vector2(1,0);
    }

    //Right button is not pressed down
    public void RightIsUp(){
        moveInput = new Vector2(0,0);
    }

    //Left button is pressed down
    public void LeftIsDown(){
        moveInput = new Vector2(-1,0);
    }

    //Left button is not pressed down
    public void LeftIsUp(){
        moveInput = new Vector2(0,0);
    }
 
}