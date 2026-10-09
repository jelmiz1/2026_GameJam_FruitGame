using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
  
    public float movementSpeed = 10;
    private Vector2 moveAmount;
    public CharacterController characterController;
    public PlayerInput playerInput;
    private Vector3 moveInput;
    private Vector3 playerVelocity;
    public Rigidbody rb;
    public float jumpHeight = 2f;
    private float gravity = -9.81f;
    public GameObject groundCheckEmpty;
    bool groundCheck;
    public FruitSpawner fruitSpawner;
    public float health = 3f;
    public float invulnerableTime = 1f;
    float invulnerableTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        FaceMoveDirection();
        invulnerableTimer -= Time.deltaTime;
    }

    public void OnMove(InputValue inputValue)
    {
        moveAmount = inputValue.Get<Vector2>();
        moveInput = new Vector3(0f, 0f, moveAmount.y);

    }
    public void OnJump()
    {

    // Jump using WasPressedThisFrame()
    if (groundCheck)
    {
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    
    }
    public void Movement()
    {
        groundCheck = characterController.isGrounded;

        if (groundCheck)
        {
            // Slight downward velocity to keep grounded stable
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
        }

        // Read input
        Vector3 move = new Vector3(0f, 0, moveAmount.y);
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
            transform.forward = move;



        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // Move
        Vector3 finalMove = move * movementSpeed + Vector3.up * playerVelocity.y;
        characterController.Move(finalMove * Time.deltaTime);
    }
    public void OnAttack(InputValue inputValue)
    {
        if (inputValue.isPressed)
            fruitSpawner.Fire();
    }
    void FaceMoveDirection()
    {
        float move = moveInput.x;   // the input axis you use for Z movement

        if (move > 0.01f)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);    // face +Z
        else if (move < -0.01f)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);  // face -Z
                                                                  // no input: keep facing the last direction
    }
    public void TakeDamage()
    {
        if (invulnerableTimer > 0f) return;

        health -= 1;
        invulnerableTimer = invulnerableTime;
        Death();
    }
    public void Death()
    {
        if(health == 0)
        {
            Destroy(gameObject);
            //Tähän logiikka game over screeniin
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.CompareTag("Enemy"))
        {
            TakeDamage();
            Debug.Log("Hit: " + hit.collider.name + " tag: " + hit.collider.tag);
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
            TakeDamage();
    }


}
