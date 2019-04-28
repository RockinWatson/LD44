using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestFX : MonoBehaviour {

    [SerializeField]
    private float _lifetime = 1.5f;
    private float _lifeTimer = 0.0f;

    private void Awake()
    {
        _lifeTimer = _lifetime;
    }

    private void Update()
    {
        _lifeTimer -= Time.deltaTime;
        if(_lifeTimer < 0f)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
