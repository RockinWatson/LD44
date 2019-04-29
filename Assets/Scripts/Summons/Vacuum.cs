using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : PursuerSummon {
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            //enemy.Kill();
            enemy.Damage(2f);
            KillPause();
        }
    }

    public void Attack()
    {
        Enemy target = GetTarget();
        if (!target || !target.gameObject.activeSelf) return;
        if ((target.transform.position - this.transform.position).sqrMagnitude < 1f)
        {
            target.Damage(2f);
            KillPause();
        }
    }
}
