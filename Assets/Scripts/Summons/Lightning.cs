using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : Summon {
    [SerializeField]
    private GameObject _FX = null;

    [SerializeField]
    private float _effectRadius = 2f;

    [SerializeField]
    private float _stunTime = 2f;

    private RaycastHit2D[] _hits = new RaycastHit2D[10];

    private void Start()
    {
        Attack();
    }

    private void Attack()
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
                        enemy.Stun(_stunTime);

                        GameObject go = Instantiate(_FX, enemy.transform);
                        Destroy(go, _stunTime);
                    }
                }
            }
        }
    }
}
