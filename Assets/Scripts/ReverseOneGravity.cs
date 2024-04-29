using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseOneGravity : MonoBehaviour
{
    [SerializeField] private float _upForce;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _spellDuration;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void ReverseGravity()
    {
        if (_rb.useGravity == true)
        {
            _rb.useGravity = false;
            _rb.AddForce(Vector3.up * _upForce);
        }
        else if (_rb.useGravity == false)
            _rb.useGravity = true;
    }

    public IEnumerator StartSpell()
    {
        ReverseGravity();
        yield return new WaitForSeconds(_spellDuration);
        ReverseGravity();
    }

    public void StartStartSpell()
    {
        StartCoroutine(StartSpell());
    }
}
