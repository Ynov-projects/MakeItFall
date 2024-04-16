using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseOneGravity : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Vector3 _gravityForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
        }
    }
}
