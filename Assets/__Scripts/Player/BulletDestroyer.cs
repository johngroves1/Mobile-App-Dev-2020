using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D externalObject)
    {
        var bullet = externalObject.GetComponent<Bullet>();  // return a value or null
        if(bullet)
        {
            Destroy(bullet.gameObject);
        }

        var enemy = externalObject.GetComponent<Enemy>();
        if (enemy)
        {
            Destroy(enemy.gameObject);
        }

        var enemyBullet = externalObject.GetComponent<EnemyBullet>();
        if (enemyBullet)
        {
            Destroy(enemyBullet.gameObject);
        }
    }
}
