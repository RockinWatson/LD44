using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float _runSpeed = 0f;
    [Range(0, .3f)]
    [SerializeField]
    private float _smoothing = .05f;

    private Rigidbody2D _rigidBody;
    private Vector3 _velocity = Vector3.zero;
    Vector3 targetVelocity;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        targetVelocity = new Vector3(Input.GetAxis("Horizontal") * _runSpeed, Input.GetAxis("Vertical") * _runSpeed, 0.0f);
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, targetVelocity, ref _velocity, _smoothing);
    }
}
