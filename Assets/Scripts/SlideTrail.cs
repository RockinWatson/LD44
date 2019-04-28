using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTrail : MonoBehaviour {

    [SerializeField]
    private float _lifeTime = 5.0f;
    private float _life = 0.0f;

    [SerializeField]
    private float _attackRadius = 2f;

    static private RaycastHit2D[] _hits = new RaycastHit2D[10];

    private LineRenderer _lineRenderer = null;

    private PlayerController _controller = null;
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _endPos = Vector3.zero;

    private bool _isRunning = true;

    private void Awake()
    {
        _lineRenderer = this.GetComponent<LineRenderer>();
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    public void Initialize(PlayerController playerController)
    {
        _isRunning = true;

        _controller = playerController;

        _startPos = _endPos = _controller.GetRigidbody().position;
        //_endPos = endPos;
        _startPos.z = -1.0f;

        _lineRenderer.SetPosition(0, _startPos);
        _lineRenderer.SetPosition(1, _endPos);
    }
	
	// Update is called once per frame
	private void Update () {
        //@TODO: Check if controller is slowed down enough to cut off the trail.
        if(_isRunning)
        {
            Rigidbody2D rb = _controller.GetRigidbody();
            _endPos = rb.position;
            _endPos.z = -1.0f;
            _lineRenderer.SetPosition(1, _endPos);
            if (rb.velocity.sqrMagnitude < _controller.GetRunSpeed())
            {
                _isRunning = false;
                _life = _lifeTime;

                AttackEnemies();
            }
        } else
        {
            //@TODO: Based on lifetime, kill this sucka.
            _life -= Time.deltaTime;
            if (_life < 0.0f)
            {
                _life = 0.0f;

                Destroy(this.gameObject);
            }
            Color color = _lineRenderer.endColor;
            color.a = (_life / _lifeTime);
            color.a = Mathf.Max(0.0f, color.a - 0.15f);
            _lineRenderer.endColor = color;
            color.a = Mathf.Max(0.0f, color.a - 0.35f);
            _lineRenderer.startColor = color;
        }
	}

    private void AttackEnemies()
    {
        float distance = Vector2.Distance(_startPos, _endPos);
        int count = Physics2D.CircleCast(_startPos, _attackRadius, _endPos - _startPos, new ContactFilter2D(), _hits, distance);
        if(count > 0)
        {
            foreach(RaycastHit2D hit in _hits)
            {
                if(hit && hit.transform)
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if(enemy)
                    {
                        enemy.Kill();
                    }
                }
            }
        }
    }
}
