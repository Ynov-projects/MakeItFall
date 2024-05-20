using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody _rb;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private Animator animator;

    [SerializeField] private float howManyTimeForRotation;

    private int _numberOfCollidingItems = 0;

    public static PlayerMovement Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    void Update()
    {
        //float horizontalMovement = Input.GetAxis("Horizontal");

        float horizontalMovement = GameManager.input.Gameplay.Move.ReadValue<Vector2>().x;

        Vector3 CurrentSpeed = _rb.velocity;
        Vector3 tempSpeed = CurrentSpeed;

        // Avancer
        if (Mathf.Abs(horizontalMovement) > 0.3f)
            tempSpeed = transform.forward * horizontalMovement * _speed;

        tempSpeed.y = CurrentSpeed.y;
        _rb.velocity = tempSpeed;

        // 0 => Reculer, .33 => repos, .66 => marcher, 1 => Courir
        animator.SetFloat("Speed", _rb.velocity.x < 0 ? 0 : .33f + (_rb.velocity.magnitude / 15));

        if (GameManager.input.Gameplay.Jump.triggered && _numberOfCollidingItems > 0) _rb.AddForce(new Vector3(0, _jumpForce, 0));

        if (_rb.velocity.y < -1) _rb.AddForce(Physics.gravity * Time.deltaTime * 50);
    }

    // rotation de bas en haut et de haut en bas
    public void rotatePlayer()
    {
        int sens = Physics.gravity.y > 0 ? 1 : -1;
        _rb.AddForce(0, sens * _jumpForce, 0);
        //rotation sens y
        transform.RotateAround(transform.position, Vector3.up, 180f);
        StartCoroutine(totalRotation());
    }

    private IEnumerator totalRotation()
    {
        yield return new WaitForSeconds(.2f);
        transform.RotateAround(transform.position, Vector3.forward, 180f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player" && !other.isTrigger)
            _numberOfCollidingItems++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != "Player" && !other.isTrigger) _numberOfCollidingItems--;
    }
}