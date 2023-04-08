using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
// NOTE: The movement for this script uses the new InputSystem. The player needs to have a PlayerInput
// component added and the Behaviour should be set to Send Messages so that the OnMove and OnFire methods
// actually trigger
 
public class PlayerController : MonoBehaviour
{

    //player assets
    SpriteRenderer player;
    Animator animator;
    [SerializeField] Sprite playerRight;
    [SerializeField] Sprite playerLeft;
    [SerializeField] Sprite playerFront;
    [SerializeField] Sprite playerBack;

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
            else if(moveInput.x == 1){
                    TerminateAnimations();
                    animator.SetBool("walk_right", success);
                    Invoke("RestingRight", animator.GetCurrentAnimatorStateInfo(0).length);
            }
            else if(moveInput.x == -1){
                    TerminateAnimations();
                    animator.SetBool("walk_left", success);
                    Invoke("RestingLeft", animator.GetCurrentAnimatorStateInfo(0).length);
            }
            else if(moveInput.y == -1){
                    TerminateAnimations();
                    animator.SetBool("walk_front", success);
                    Invoke("RestingFront", animator.GetCurrentAnimatorStateInfo(0).length);
            }
            else if(moveInput.y == 1){
                    TerminateAnimations();
                    animator.SetBool("walk_back", success);
                    Invoke("RestingBack", animator.GetCurrentAnimatorStateInfo(0).length);
            }
        }
        else {
            TerminateAnimations();
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

    public void TerminateAnimations(){
        animator.SetBool("walk_right", false);
        animator.SetBool("walk_left", false);
        animator.SetBool("walk_front", false);
        animator.SetBool("walk_back", false);
    }

    //Following methods set the player sprite according to movement
    private void RestingRight(){
        player.sprite = playerRight;
    }

    private void RestingLeft(){
        player.sprite = playerLeft;
    }

    private void RestingFront(){
        player.sprite = playerFront;
    }

    private void RestingBack(){
        player.sprite = playerBack;
    }

    //Following methods set up moveInput for buttons on screen

    //Up button is pressed down
    public void UpIsDown(){
        TerminateAnimations();
        animator.SetBool("walk_back", true);
        moveInput = new Vector2(0,1);
    }

    //Up button is not pressed down
    public void UpIsUp(){
        TerminateAnimations();
        moveInput = new Vector2(0,0);
    }

    //Down button is pressed down
    public void DownIsDown(){
        TerminateAnimations();
        animator.SetBool("walk_front", true);
        moveInput = new Vector2(0,-1);
    }

    //Down button is not pressed down
    public void DownIsUp(){
        TerminateAnimations();
        moveInput = new Vector2(0,0);
    }

    //Right button is pressed down
    public void RightIsDown(){
        TerminateAnimations();
        animator.SetBool("walk_right", true);
        moveInput = new Vector2(1,0);
    }

    //Right button is not pressed down
    public void RightIsUp(){
        TerminateAnimations();
        moveInput = new Vector2(0,0);
    }

    //Left button is pressed down
    public void LeftIsDown(){
        TerminateAnimations();
        animator.SetBool("walk_left", true);
        moveInput = new Vector2(-1,0);
    }

    //Left button is not pressed down
    public void LeftIsUp(){
        TerminateAnimations();
        moveInput = new Vector2(0,0);
    }
 
}