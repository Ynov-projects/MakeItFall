using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseGravityScript : MonoBehaviour
{
    [SerializeField] private float _gravityForceY;
    [SerializeField] private float _pushForce;
    [SerializeField] private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //_rb.AddForce(new Vector3(0, _pushForce, 0));
            Physics.gravity = new Vector3(0, Physics.gravity.y > 1f ? - _gravityForceY : _gravityForceY, 0);
        }
    }
}
