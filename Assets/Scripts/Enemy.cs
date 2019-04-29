using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _corpse = null;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float _health = 10f;
    private float _maxHealth;

    private Rigidbody2D _rigidBody;
    private SpriteRenderer _renderer;

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
        Player player = Player.Get();
        transform.position += (player.transform.position - transform.position).normalized * speed * Time.fixedDeltaTime;
    }

    private void Reset()
    {
        this.gameObject.SetActive(false);
        _health = _maxHealth;
    }

    public void Kill()
    {
        Instantiate(_corpse, this.transform.position, Quaternion.identity);

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
        //color.r = (1f - (_health/_maxHealth));
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
}
