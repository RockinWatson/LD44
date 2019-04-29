using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Summon : MonoBehaviour {

    [SerializeField]
    private GameObject _deathFX = null;

    [SerializeField]
    private float _lifetime = 10f;
    private float _lifeTimer = 0f;

    [SerializeField]
    private float _healthCost = 10f;

    public enum Type
    {

    };

    private void Awake()
    {
        _lifeTimer = _lifetime;
    }

    virtual protected void Update()
    {
        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer < 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if(_deathFX)
        {
            Instantiate(_deathFX, this.transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }
}
