using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    Rigidbody2D rb;
    Animator animator;

    Vector2 moveInput;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetChild(PlayerPrefs.GetInt("SelectedCharacter", 0)).GetComponent<Animator>();
    }

    void Update(){
        Run();
    }


    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value){
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && value.isPressed){
            rb.linearVelocity += new Vector2(0f, jumpForce);            
        }        
    }

    private void IsJumping(){
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders")) ){
            animator.SetBool("isJumping", true);            
        }else{
            animator.SetBool("isJumping", false);            
        }
        
    }


    private void Run(){
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
       
        animator.SetBool("isRunning", moveInput.x != 0f);

        if (moveInput.x < 0f){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else if (moveInput.x > 0f){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }   

    private void ClimbLadder(){
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders"))){
            rb.gravityScale = gravityScaleAtStart;
            animator.SetBool("isClimbing", false);
            return;    
        }

        rb.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2(rb.linearVelocity.x, moveInput.y * moveSpeed);
        rb.linearVelocity = climbVelocity;

        animator.SetBool("isClimbing", moveInput.y != 0f);
    }

    private void OnJump(InputValue value){
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && value.isPressed){
            rb.linearVelocity = new Vector2(0f, jumpForce);
        }
    }

    private void IsJumping(){
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            animator.SetBool("isJumping", true);
        }else{
            animator.SetBool("isJumping", false);
        }
    }
}
