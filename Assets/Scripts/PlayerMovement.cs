using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _acceleration;

    private int _nbrColliderUnder = 0;

    [SerializeField] private Rigidbody _rb;

    #region "singleton"
    public static PlayerMovement Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }
    #endregion

    void Update()
    {
        float speedDelta = _movementSpeed * Time.deltaTime;
        Vector3 CurrentSpeed = _rb.velocity;
        Vector3 tempSpeed = CurrentSpeed;

        float running = Input.GetKey(KeyCode.LeftShift) ? 1.75f : 1f;
        if (Input.GetKey(KeyCode.D))
            tempSpeed = transform.forward * speedDelta * running;
        if (Input.GetKey(KeyCode.A))
            tempSpeed = -transform.forward * speedDelta;

        tempSpeed.y = CurrentSpeed.y;

        _rb.velocity = Vector3.Lerp(_rb.velocity, tempSpeed, Time.deltaTime * _acceleration);

        if (Input.GetKeyDown(KeyCode.Space) && _nbrColliderUnder > 0) _rb.AddForce(new Vector3(0, _jumpForce, 0));

        if (_rb.velocity.y < -1)
            _rb.AddForce(Physics.gravity * Time.deltaTime * 100);

        float moveZ = _rb.velocity.x < 0 ? 0 : .33f + (_rb.velocity.magnitude / 15) + (running > 1f ? .33f : 0);
        PlayerScript.Instance.GetAnimator().SetFloat("Speed", moveZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        _nbrColliderUnder++;
    }

    private void OnTriggerExit(Collider other)
    {
        _nbrColliderUnder--;
    }
}
