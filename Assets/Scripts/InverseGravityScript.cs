using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseGravityScript : MonoBehaviour
{
    [SerializeField] private float _gravityForceY;
    [SerializeField] private float _pushForce;
    [SerializeField] private Rigidbody _rb;

    public void ReverseAllGravity()
    {
        Physics.gravity = new Vector3(0, Physics.gravity.y > 1f ? -_gravityForceY : _gravityForceY, 0);
    }
}
