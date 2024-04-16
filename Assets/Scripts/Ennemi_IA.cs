using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi_IA : MonoBehaviour
{
    Transform PlayerTransform;
    private Vector3 _startPosition;
    [SerializeField] float _RotationSpeed = 3.0f, _MoveSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _startPosition = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (PlayerTransform && other.CompareTag("Player"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(PlayerTransform.position - transform.position),
                    _RotationSpeed * Time.deltaTime);

            transform.position += transform.forward * _MoveSpeed * Time.deltaTime;
        }
    }
}
