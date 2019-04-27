using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTrail : MonoBehaviour {

    [SerializeField]
    private float _lifeTime = 5.0f;
    private float _life = 0.0f;

    private LineRenderer _lineRenderer = null;

    private PlayerController _controller = null;
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _endPos = Vector3.zero;

    private bool _isRunning = true;

    private void Awake()
    {
        _lineRenderer = this.GetComponent<LineRenderer>();
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
            _lineRenderer.startColor = color;
            _lineRenderer.endColor = color;
            //_lineRenderer.SetColors(color, color);

            //_lineRenderer.alignment = LineAlignment.View;
        }
	}
}
