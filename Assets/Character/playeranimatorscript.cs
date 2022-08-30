using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimatorscript : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("iswalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isrunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        //if player presses w key
        if (!isWalking && forwardPressed)
        {
            //then set the iswalking boolean to be true
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        //if player is walking and presses left shift
        if (!isrunning && forwardPressed && runPressed)
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isrunning && !forwardPressed || !runPressed)
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
