using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuerSummon : Summon
{
    [SerializeField]
    private float _speed = 5f;

    private Enemy _target = null;
    public Enemy GetTarget() { return _target; }

    readonly float _targetRefindTime = 4f;
    private float _targetTimer = 0f;

    private Vector3 _targetVecloity;

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

            _targetVecloity = (_target.transform.position - transform.position).normalized * _speed * Time.fixedDeltaTime;
            transform.position += _targetVecloity;
        }
        else
        {
            SetNearestTarget();
        }

        UpdateHeading();
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
            if (enemy.gameObject.activeSelf == false) continue;
            float distance = (this.transform.position - enemy.transform.position).sqrMagnitude;
            if (distance < bestDistance)
            {
                nearest = enemy;
                bestDistance = distance;
            }
        }
        return nearest;
    }

    private void UpdateHeading()
    {
        float xDirection = _targetVecloity.x;
        const float epsilon = 0.01f;
        if (Mathf.Abs(xDirection) > epsilon)
        {
            Vector3 scale = this.transform.localScale;
            scale.x = (_targetVecloity.x <= 0f) ? -1f : 1f;
            this.transform.localScale = scale;
        }
    }
}
