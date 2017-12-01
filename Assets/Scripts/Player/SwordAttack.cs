using UnityEngine;

public class SwordAttack : MonoBehaviour {
    
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponentInParent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CharacterManager.Instance == null)
            return;

        foreach (var enemy in CharacterManager.Instance.enemiesList)
        {
            if (collision.gameObject == enemy.gameObject && playerStats.inAttack)
            {
                EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
                enemyStats.healthPoints -= playerStats.damage;
                if (playerStats.ultiPoints < playerStats.ultiCost && playerStats.ultiState == PlayerStats.UltiState.charging)
                    playerStats.ultiPoints += playerStats.damage;
            }
        }

        
    }
}
