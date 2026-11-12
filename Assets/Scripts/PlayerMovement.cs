using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 20f;

    Rigidbody2D rb;
    Animator animator;
    BoxCollider2D feetCollider;

    Vector2 moveInput;

    bool isJumping = false;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetChild(PlayerPrefs.GetInt("SelectedCharacter", 0)).GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
    }

    void Update(){
        Run();
        IsJumping();
    }


    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();    
    }

    private void Run(){
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("isRunning", moveInput.x != 0f);

        if (moveInput.x < 0f){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (moveInput.x > 0f){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
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
