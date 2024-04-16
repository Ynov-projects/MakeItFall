using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*public CharacterController controller;
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
    }*/

    [SerializeField]
    private float _movementSpeed;

    /*[SerializeField]
    private float _jumpForce;*/

    [SerializeField]
    private float _acceleration;

    //private int _nbrColliderUnder = 0;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private Animator animator;

    void Update()
    {
        float speedDelta = _movementSpeed * Time.deltaTime;
        Vector3 CurrentSpeed = _rb.velocity;

        Vector3 tempSpeed = CurrentSpeed;

        if (Input.GetKey(KeyCode.D))
        {
            tempSpeed = transform.forward * speedDelta;
        }
        if (Input.GetKey(KeyCode.A))
            tempSpeed = -transform.forward * speedDelta;

        tempSpeed.y = CurrentSpeed.y;

        _rb.velocity = Vector3.Lerp(_rb.velocity, tempSpeed, Time.deltaTime * _acceleration);

        /*if (Input.GetKeyDown(KeyCode.Space) && _nbrColliderUnder > 0)
        {
            _rb.AddForce(new Vector3(0, _jumpForce, 0));
        }*/

        if (_rb.velocity.y < -1)
            _rb.AddForce(Physics.gravity * Time.deltaTime * 100);

        animator.SetFloat("Speed", _rb.velocity.magnitude);

        /*if (Input.GetMouseButtonDown(0))
            _animator.SetTrigger("Active");*/
    }

    /*private void OnTriggerEnter(Collider other)
    {
        _nbrColliderUnder++;
    }

    private void OnTriggerExit(Collider other)
    {
        _nbrColliderUnder--;
    }*/
}
