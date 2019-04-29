using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFX : MonoBehaviour {

    [SerializeField]
    private GameObject _corpse = null;

    [SerializeField]
    private float _animSpeed = 1f;

    private void Awake()
    {
        Animator animator = this.GetComponent<Animator>();
        animator.speed = _animSpeed;
    }

    public void Corpse()
    {
        SpawnCorpse();

        Destroy(this.gameObject);
    }

    private void SpawnCorpse()
    {
        if (_corpse)
        {
            Instantiate(_corpse, this.transform.position, Quaternion.identity);
        }
    }
}
