using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi_IA : MonoBehaviour
{
    Transform PlayerTransform;
    private Vector3 _startPosition;
    [SerializeField] float _RotationSpeed = 3.0f, _MoveSpeed = 3.0f;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_rb.velocity.y < 150)
            _rb.AddForce(Physics.gravity * Time.deltaTime * 100);
    }

    private void OnTriggerStay(Collider other)
    {
        if (PlayerTransform && other.CompareTag("Player"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(PlayerTransform.position - transform.position),
                    _RotationSpeed * Time.deltaTime);
            transform.position += transform.forward * _MoveSpeed * Time.deltaTime;
            animator.SetFloat("Speed", 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetFloat("Speed", 0);
    }
}
