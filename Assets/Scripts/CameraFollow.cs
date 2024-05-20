using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;
    public Vector3 posOffsetReversed;
    private bool reversed;

    private Vector3 velocity;

    public static CameraFollow instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    public void changeGravity()
    {
        reversed = !reversed;
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + (reversed ? posOffsetReversed : posOffset), ref velocity, timeOffset);
    }
}
