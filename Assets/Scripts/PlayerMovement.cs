using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MovementSpeed;
    public float GroundDrag;

    public float JumpForce;
    public float JumpCooldown;
    public float AirMultiplier;
    public bool ReadyToJump;

    [Header("Ground Check")]
    public float PlayerHeight;
    public LayerMask WhatIsGround;
    public bool IsGrounded;

    [Header("Keybinds")]
    public KeyCode JumpKey = KeyCode.Space;

    public Transform Orientation;
    float HorizontalInput;
    float VerticalInput;
    Vector3 MoveDirection;
    Rigidbody RigidBody;

    void Start() {
        RigidBody = GetComponent<Rigidbody>();
        RigidBody.freezeRotation = true;
        
    }

    void Update() {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, WhatIsGround);

        Inputs();
        SpeedControl();
    
        if (IsGrounded)
            RigidBody.drag = GroundDrag;
        else
            RigidBody.drag = 0f;
    }

    void FixedUpdate() {
        MovePlayer();
    }

    void Inputs() {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    
        if(Input.GetKey(JumpKey) && ReadyToJump && IsGrounded) {
            ReadyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), JumpCooldown);
        }
    }

    void MovePlayer() {
        MoveDirection = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;
        
        if (IsGrounded)
            RigidBody.AddForce(MoveDirection * MovementSpeed * 10f, ForceMode.Force);
   
        else if (!IsGrounded)
            RigidBody.AddForce(MoveDirection * MovementSpeed * 10f * AirMultiplier, ForceMode.Force);
    }

    void SpeedControl() {
        Vector3 FlatVelocity = new Vector3(RigidBody.velocity.x, 0f, RigidBody.velocity.z);

        if(FlatVelocity.magnitude > MovementSpeed) {
            Vector3 LimitedVelocity = FlatVelocity.normalized * MovementSpeed;
            RigidBody.velocity = new Vector3(LimitedVelocity.x, RigidBody.velocity.y, LimitedVelocity.z); 
        }
    }

    void Jump() {
        RigidBody.velocity = new Vector3(RigidBody.velocity.x, 0f, RigidBody.velocity.z);

        RigidBody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    void ResetJump() {
        ReadyToJump = true;
    }
}
