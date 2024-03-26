using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float baseSpeed = 12f;
    public float jumpHeight = 3f;
    public float sprintSpeed = 5f;

    float speedBoost = 1f;
    Vector3 velocity;

    [SerializeField] private Animator animator;
    
    void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float z = Input.GetAxis("Horizontal");

        bool running = Input.GetButton("Fire3");
        if (running && z > 0) 
            speedBoost = sprintSpeed;
        else
            speedBoost = 1f;

        Vector3 move = transform.forward * z;

        // Blend tree spécifique, se référer directement onglet animations
        float moveZ = move.z < 0 ? 0 : .33f + (move.z / 3) + (running ? .33f : 0);
        animator.SetFloat("Speed", moveZ);

        controller.Move(move * (baseSpeed + speedBoost) * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
