using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private Animator animator;

    private int _numberOfCollidingItems = 0;

    public static PlayerMovement Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        Vector3 CurrentSpeed = _rb.velocity;
        Vector3 tempSpeed = CurrentSpeed;

        // Rotation
        if (Mathf.Abs(horizontalMovement) > 0.3f)
        {
            float running = Input.GetKey(KeyCode.LeftShift) && horizontalMovement > 0 ? 2f : 1f;
            tempSpeed = transform.forward * horizontalMovement * running * _speed;
        }

        tempSpeed.y = CurrentSpeed.y;
        _rb.velocity = tempSpeed;

        animator.SetFloat("Speed", _rb.velocity.x < 0 ? 0 : .33f + (_rb.velocity.x / 6));

        if (Input.GetKeyDown(KeyCode.Space) && _numberOfCollidingItems > 0) _rb.AddForce(new Vector3(0, _jumpForce, 0));

        if (_rb.velocity.y < -1) _rb.AddForce(Physics.gravity * Time.deltaTime * 50);
    }

    private void OnTriggerEnter(Collider other)
    {
        _numberOfCollidingItems++;
    }

    private void OnTriggerExit(Collider other)
    {
        _numberOfCollidingItems--;
    }
}