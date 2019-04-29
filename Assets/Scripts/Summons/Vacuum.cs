using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : PursuerSummon {
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Kill();
            KillPause();
        }
    }
}
