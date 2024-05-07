using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
            PlayerHealth.Instance.TakeDamage(damage);
    }
}
