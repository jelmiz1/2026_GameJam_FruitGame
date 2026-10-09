using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10;
    private Vector2 moveAmount;
    public CharacterController characterController;
    public PlayerInput playerInput;
    private Vector3 moveInput;
    public Rigidbody rb;
    public float jumpForce = 10f;
    private float gravity = 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(movementSpeed * Time.deltaTime * moveInput );

    }
    public void OnMove(InputValue inputValue)
    {
        moveAmount = inputValue.Get<Vector2>();
        moveInput = new Vector3(moveAmount.x, 0f, moveAmount.y);

    }
    public void OnJump()
    {

        moveInput = new Vector3(0f, jumpForce, 0f);
    }
}
