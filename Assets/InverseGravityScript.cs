using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseGravityScript : MonoBehaviour
{
    [SerializeField] private float _gravityForceY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            Physics.gravity = new Vector3(0, -5.0F, 0);
        }
    }
}
