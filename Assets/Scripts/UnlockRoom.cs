using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockRoom : MonoBehaviour
{
    [SerializeField] GameObject gameObject1;
    [SerializeField] GameObject gameObject2;
    [SerializeField] GameObject gameObject3;
    [SerializeField] GameObject gameObject4;

    private void Start()
    {
        gameObject1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY
             | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY
             | RigidbodyConstraints.FreezeRotationZ;
        gameObject2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY
             | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY
             | RigidbodyConstraints.FreezeRotationZ;
        gameObject3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY
             | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY
             | RigidbodyConstraints.FreezeRotationZ;
        gameObject4.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY
             | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY
             | RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationY;
        gameObject2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject4.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
