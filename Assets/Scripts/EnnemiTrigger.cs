using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiTrigger : MonoBehaviour
{
    private Transform _playerTrasform;
    private Vector3 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        _playerTrasform = other.transform;
        GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(_playerTrasform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_playerTrasform)
            GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(_playerTrasform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        _playerTrasform = null;
        GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(_startPosition);
    }*/

    // Update is called once per frame
    void Update()
    {

    }
}
