using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuerSummon : Summon
{
    [SerializeField]
    private float _speed = 5f;

    private Enemy _target = null;

    private float _targetRefindTime = 4f;
    private float _targetTimer = 0f;

    //override protected void Update()
    //{
    //    base.Update();

    //    //if(!_target)
    //    //{
    //    //    SetNearestTarget();
    //    //}
    //    //UpdateMovement();
    //}

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (_target)
        {
            _targetTimer -= Time.fixedDeltaTime;
            if (_targetTimer < 0f)
            {
                SetNearestTarget();
            }

            transform.position += (_target.transform.position - transform.position).normalized * _speed * Time.fixedDeltaTime;
        }
        else
        {
            SetNearestTarget();
        }
    }

    private void SetNearestTarget()
    {
        _target = FindNearestTarget();
        _targetTimer = _targetRefindTime;
    }

    private Enemy FindNearestTarget()
    {
        Enemy nearest = null;
        float bestDistance = Mathf.Infinity;
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            float distance = (this.transform.position - enemy.transform.position).sqrMagnitude;
            if (distance < bestDistance)
            {
                nearest = enemy;
                bestDistance = distance;
            }
        }
        return nearest;
    }
}
