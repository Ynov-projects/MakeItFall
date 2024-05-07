using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi_IA : MonoBehaviour
{
    private Transform PlayerTransform;
    [SerializeField] float _RotationSpeed = 3.0f, _MoveSpeed = 3.0f;
    [SerializeField] private EnemyScript enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = PlayerMovement.Instance.transform;
    }

    private void Update()
    {
        if (enemyScript._rb.velocity.y < 150)
            enemyScript._rb.AddForce(Physics.gravity * Time.deltaTime * 100);
    }

    private void OnTriggerStay(Collider other)
    {
        if (PlayerTransform && other.CompareTag("Player"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(PlayerTransform.position - transform.position),
                    _RotationSpeed * Time.deltaTime);
            transform.position += transform.forward * _MoveSpeed * Time.deltaTime;
            //enemyScript.animator.SetFloat("Speed", 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //enemyScript.animator.SetFloat("Speed", 0);
    }
}
