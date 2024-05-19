using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            GameManager.Instance.ChangeCheckpoint(transform.position);
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
