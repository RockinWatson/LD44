using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : Summon {
    [SerializeField]
    private GameObject _FX = null;

    [SerializeField]
    private float _effectRadius = 2f;

    private RaycastHit2D[] _hits = new RaycastHit2D[10];

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Detonate();
        }
    }

    private void Detonate()
    {
        int count = Physics2D.CircleCast(this.transform.position, _effectRadius, Vector2.zero, new ContactFilter2D(), _hits);
        if (count > 0)
        {
            foreach (RaycastHit2D hit in _hits)
            {
                if (hit && hit.transform)
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy && enemy.IsEnabled())
                    {
                        enemy.Kill();
                    }
                }
            }
        }

        //@TODO: Setoff FX

        Destroy(this.gameObject);
    }
}
