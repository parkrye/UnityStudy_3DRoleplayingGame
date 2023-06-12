using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    Vector3 moveDir;

    [SerializeField] float walkSpeed, runSpeed, currentSpeed, jumpSpeed, ySpeed;
    [SerializeField] bool walk;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        IsGround();
        Move();
        Jump();
    }

    void Move()
    {

        if (moveDir.magnitude == 0f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, 0.1f);
            animator.SetBool("Move", false);
            animator.SetFloat("Foward", currentSpeed);
            animator.SetFloat("Side", moveDir.x * currentSpeed);
            return;
        }
        
        if (walk)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, 0.1f);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, 0.1f);
        }

        animator.SetBool("Move", true);
        animator.SetFloat("Foward", currentSpeed);
        animator.SetFloat("Side", moveDir.x * currentSpeed);

        Vector3 fowardVec = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z).normalized;
        Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z).normalized;

        controller.Move(moveDir.z * fowardVec * currentSpeed * Time.deltaTime);
        controller.Move(moveDir.x * rightVec * currentSpeed * Time.deltaTime);

        Quaternion lookRotation = Quaternion.LookRotation(fowardVec * moveDir.z + rightVec * moveDir.x);
        transform.rotation = Quaternion.Lerp(lookRotation, transform.rotation, 0.2f);
    }

    void Jump()
    {
        controller.Move(transform.up * ySpeed * Time.deltaTime);
        if (!animator.GetBool("Ground"))
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }
    }

    void OnMove(InputValue inputValue)
    {
        moveDir.x = inputValue.Get<Vector2>().x;
        moveDir.z = inputValue.Get<Vector2>().y;
    }

    void OnJump(InputValue inputValue)
    {
        if (animator.GetBool("Ground"))
        {
            ySpeed = jumpSpeed;
        }
    }

    void IsGround()
    {
        if (controller.isGrounded)
        {
            animator.SetBool("Ground", true);
            return;
        }
        animator.SetBool("Ground", false);
    }

    void OnWalk(InputValue inputValue)
    {
        walk = inputValue.isPressed;
    }
}
