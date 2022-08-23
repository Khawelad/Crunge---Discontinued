using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform Orientation;
    public Transform Player;
    public Transform PlayerObj;
    public Rigidbody RigidBody;
    public GameObject FreeLookCamera;
    public GameObject CombatCamera;
    public GameObject TopDownCamera;

    public float RotationSpeed;

    public Transform CombatLootAt;
    public CameraStyle CurrentStyle;
    public enum CameraStyle 
    {
        Basic,
        Combat,
        TopDown
    }

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Combat);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchCameraStyle(CameraStyle.TopDown);

        Vector3 ViewDirection = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
        Orientation.forward = ViewDirection;
    
        if(CurrentStyle == CameraStyle.Basic || CurrentStyle == CameraStyle.TopDown) {
            float HorizontalInput = Input.GetAxis("Horizontal");
            float VerticalInput = Input.GetAxis("Vertical");
            Vector3 InputDirection = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;
        
            if(InputDirection != Vector3.zero) {
                PlayerObj.forward = Vector3.Slerp(PlayerObj.forward, InputDirection.normalized, Time.deltaTime * RotationSpeed);
            }
        }
        else if (CurrentStyle == CameraStyle.Combat)
        {
            Vector3 DirectionToCombatLookAt = CombatLootAt.position - new Vector3(transform.position.x, CombatLootAt.position.y, transform.position.z);
            Orientation.forward = DirectionToCombatLookAt.normalized;
            PlayerObj.forward = DirectionToCombatLookAt.normalized;
        }
    }

    void SwitchCameraStyle(CameraStyle NewStyle) {
        CombatCamera.SetActive(false);
        FreeLookCamera.SetActive(false);
        TopDownCamera.SetActive(false);

        if (NewStyle == CameraStyle.Basic) FreeLookCamera.SetActive(true);
        if (NewStyle == CameraStyle.Combat) CombatCamera.SetActive(true);
        if (NewStyle == CameraStyle.TopDown) TopDownCamera.SetActive(true);

        CurrentStyle = NewStyle;
    }
}
