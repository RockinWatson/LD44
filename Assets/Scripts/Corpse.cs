using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour {

    [SerializeField]
    private GameObject _harvestFX = null;
    private HarvestFX _FX = null;

    [SerializeField]
    private float _harvestValue = 30f;

    private bool _isHarvested = false;
    public bool IsHarvested() { return _isHarvested; }

    [SerializeField]
    private float _lifetime = 5.0f;
    private float _lifeTimer = 0.0f;
    [SerializeField]
    private float _deathTime = 3.0f;
    private float _deathTimer = 0.0f;

    private SpriteRenderer _renderer = null;

    private void Awake()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _lifeTimer = _lifetime;
        _deathTimer = _deathTime;
    }

    // Update is called once per frame
    void Update () {
        if (_isHarvested)
        {
            UpdateDeathState();
        } else
        {
            _lifeTimer -= Time.deltaTime;
            if (_lifeTimer < 0f)
            {
                UpdateDeathState();
            }
        }
    }

    private void UpdateDeathState()
    {
        _deathTimer -= Time.deltaTime;
        if (_deathTimer < 0f)
        {
            Destroy(this.transform.gameObject);
        }

        float scale = _deathTimer / _deathTime;
        Color color = _renderer.color;
        color.a = scale;
        _renderer.color = color;

        if(_FX)
        {
            _FX.UpdateAlpha(scale);
        }
    }

    public float Harvest()
    {
        _isHarvested = true;
        _deathTimer = _deathTime;

        GameObject go = Instantiate(_harvestFX, this.transform);
        _FX = go.GetComponent<HarvestFX>();

        Color color = _renderer.color;
        color.a = 1.0f;
        _renderer.color = color;

        return _harvestValue;
    }
}
