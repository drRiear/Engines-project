using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {
    
    [SerializeField] private PlayerStats playerStats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && playerStats.inAttack)
        {
            var enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats.healthPoints--;
        }
    }
}
