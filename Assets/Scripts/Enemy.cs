using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float _health = 10f;
    private float _maxHealth;

    [SerializeField]
    private GameObject _deathFX = null;

    private Rigidbody2D _rigidBody;
    private SpriteRenderer _renderer;

    private GameObject _target = null;
    private float _targetRefindTime = 2f;
    private float _targetRefindTimer = 0f;

    private float _stunTimer = 0f;
    public void Stun(float time) { _stunTimer = time; }

    public bool IsEnabled() { return this.gameObject.activeInHierarchy; }

    private void Awake()
    {
        _maxHealth = _health;
        _rigidBody = this.GetComponent<Rigidbody2D>();
        _renderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy)
        {
            speed = 1.0f;
        }
        UpdateTarget();
        UpdateHealthVisual();
    }

    private void FixedUpdate()
    {
        if(_stunTimer > 0f)
        {
            _stunTimer -= Time.fixedDeltaTime;
            return;
        }
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        transform.position += (_target.transform.position - transform.position).normalized * speed * Time.fixedDeltaTime;
    }

    private void Reset()
    {
        this.gameObject.SetActive(false);
        _health = _maxHealth;
    }

    public void Kill()
    {
        Instantiate(_deathFX, this.transform.position, Quaternion.identity);

        Reset();
    }

    private void OnBecameInvisible()
    {
        Reset();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "NecroCat" || collision.gameObject.tag == "Enemy")
        {
            _rigidBody.velocity = Vector3.zero;
        }
    }

    private void UpdateHealthVisual()
    {
        Color color = _renderer.color;
        float gradient = (_health / _maxHealth);
        color.g = gradient;
        color.b = gradient;
        _renderer.color = color;
    }

    public void Damage(float damage)
    {
        _health -= damage;
        if(_health < 0f)
        {
            Kill();
        }
    }

    private void UpdateTarget()
    {
        _targetRefindTimer -= Time.deltaTime;
        if(_targetRefindTimer < 0f)
        {
            SetTarget();
            _targetRefindTimer = _targetRefindTime;
        }
    }

    private void SetTarget()
    {
        float bestDistance = Mathf.Infinity;
        Decoy[] decoys = FindObjectsOfType<Decoy>();
        foreach(Decoy decoy in decoys)
        {
            if((this.transform.position - decoy.transform.position).sqrMagnitude < bestDistance)
            {
                _target = decoy.gameObject;
            }
        }
        if(!_target)
        {
            _target = Player.Get().gameObject;
        }
    }
}
