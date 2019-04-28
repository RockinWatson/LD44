using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestFX : MonoBehaviour {

    [SerializeField]
    private float _lifetime = 1.5f;
    private float _lifeTimer = 0.0f;

    [SerializeField]
    private float _harvestedLifetime = 5.0f;
    private float _harvestedTimer = 0.0f;

    private SpriteRenderer _renderer = null;

    private void Awake()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _lifeTimer = _lifetime;
        _harvestedTimer = _harvestedLifetime;
    }

    private void Update()
    {
        _lifeTimer -= Time.deltaTime;
        if(_lifeTimer < 0f)
        {
            //UpdateDeathState();
        }
    }

    private void UpdateDeathState()
    {
        _harvestedTimer -= Time.deltaTime;
        if (_harvestedTimer < 0f)
        {
            Destroy(this.transform.parent.gameObject);
        }

        UpdateAlpha(_harvestedTimer / _harvestedLifetime);
    }

    public void UpdateAlpha(float alpha)
    {
        Color color = _renderer.color;
        color.a = alpha;
        _renderer.color = color;
    }
}
