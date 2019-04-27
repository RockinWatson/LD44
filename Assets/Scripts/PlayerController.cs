using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float _runSpeed = 0f;
    public float GetRunSpeed() { return _runSpeed; }
    [Range(0, .3f)]
    [SerializeField]
    private float _smoothing = .05f;

    [SerializeField]
    private float _slideForce = 30f;
    private bool _slideGo = false;

    [SerializeField]
    private Vector2 _axisBias = Vector2.one;

    [SerializeField]
    private GameObject _slideTrail = null;

    private Rigidbody2D _rigidBody;
    public Rigidbody2D GetRigidbody() { return _rigidBody; }
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _targetVelocity = Vector3.zero;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //_targetVelocity = new Vector3(Input.GetAxis("Horizontal") * _runSpeed, Input.GetAxis("Vertical") * _runSpeed, 0.0f);
        _targetVelocity.x = Input.GetAxis("Horizontal");
        _targetVelocity.y = Input.GetAxis("Vertical");
        _targetVelocity *= _runSpeed;

        UpdatePlayerInput();
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, _targetVelocity, ref _velocity, _smoothing) * Time.fixedDeltaTime;
        
        //if(_slideGo)
        //{
        //    Slide();
        //    _slideGo = false;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            _rigidBody.velocity = Vector3.zero;
        }
    }

    private void UpdatePlayerInput()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            //_slideGo = true;
            Slide();

            //@TODO: Pump the breaks with E
            //@TODO: Maybe check state of slide, if actively going, bum
        }
    }

    private void Slide()
    {
        //@TODO: Based on directional aim, slide/skate player
        //Vector3 dir = new Vector3(Input.GetAxis("Horizontal") * _slideForce, Input.GetAxis("Vertical") * _slideForce, 0.0f);

        Vector3 dir = new Vector3(Input.GetAxis("Horizontal") * _axisBias.x, Input.GetAxis("Vertical") * _axisBias.y, 0.0f);
        _rigidBody.AddForceAtPosition(dir * _slideForce, _rigidBody.position, ForceMode2D.Impulse);

        //@TODO: Slide Trail setup
        GameObject go = Instantiate(_slideTrail);
        SlideTrail trail = go.GetComponent<SlideTrail>(); // (SlideTrail)Instantiate(_slideTrail);
        trail.Initialize(this);

    }
}
