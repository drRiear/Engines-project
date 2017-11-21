using UnityEngine;

public class SwordAttack : MonoBehaviour {
    
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponentInParent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && playerStats.inAttack)
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            enemyStats.healthPoints -= playerStats.damage;
            if (playerStats.ultiPoints < playerStats.ultiCost && playerStats.ultiState == PlayerStats.UltiState.charging)
                playerStats.ultiPoints += playerStats.damage;
        }
    }
}
