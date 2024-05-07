using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField] public int trapDamage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(trapDamage);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("collision");
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage();
            //GetComponent<EnemyScript>().TakeDamage();
        }
    }

}
