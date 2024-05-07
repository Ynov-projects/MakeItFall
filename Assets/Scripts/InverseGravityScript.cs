using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseGravityScript : MonoBehaviour
{
    [SerializeField] private float _gravityForceY;
    // [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _spellDuration;

    public void ReverseAllGravity()
    {
        Physics.gravity = new Vector3(0, Physics.gravity.y > 1f ? -_gravityForceY : _gravityForceY, 0);
    }
    public IEnumerator StartSpell()
    {
        ReverseAllGravity();
        yield return new WaitForSeconds(_spellDuration);
        ReverseAllGravity();
    }

    public void StartStartSpell()
    {
        StartCoroutine(StartSpell());
    }
}
