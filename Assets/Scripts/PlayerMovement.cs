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

        // Avancer
        float running = Input.GetKey(KeyCode.LeftShift) && horizontalMovement > .3f ? 1.75f : 1f; // Si on court vers l'avant
        if (Mathf.Abs(horizontalMovement) > 0.3f)
            tempSpeed = transform.forward * horizontalMovement * _speed * running;

        tempSpeed.y = CurrentSpeed.y;
        _rb.velocity = tempSpeed;

        // 0 => Reculer, .33 => repos, .66 => marcher, 1 => Courir
        animator.SetFloat("Speed", _rb.velocity.x < 0 ? 0 : .33f + (_rb.velocity.magnitude / 15) + (running > 1f ? .33f : 0));

        if (Input.GetKeyDown(KeyCode.Space) && _numberOfCollidingItems > 0) _rb.AddForce(new Vector3(0, _jumpForce, 0));

        if (Input.GetKeyDown(KeyCode.Space)) Debug.Log(_numberOfCollidingItems);

        if (_rb.velocity.y < -1) _rb.AddForce(Physics.gravity * Time.deltaTime * 50);
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