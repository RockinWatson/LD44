using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _corpse = null;

    [SerializeField]
    private float speed = 1.0f;

    private Rigidbody2D _rigidBody;

    //TODO: Enemy Reset

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy)
        {
            speed = 1.0f;
        }
    }

    private void FixedUpdate()
    {
        Player player = Player.Get();
        transform.position += (player.transform.position - transform.position).normalized * speed * Time.fixedDeltaTime;
    }

    private void Reset()
    {
        this.gameObject.SetActive(false);
    }

    public void Kill()
    {
        Instantiate(_corpse, this.transform.position, Quaternion.identity);

        Reset();
    }

    private void OnBeameInvisible()
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
}
