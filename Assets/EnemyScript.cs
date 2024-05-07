using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody _rb;
    public Animator animator;
    public ParticleSystem particleSystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) TakeDamage();
    }

    public void TakeDamage()
    {
        _rb.velocity = Vector3.zero;
        //animator.SetTrigger("Death");
        particleSystem.Play();
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
