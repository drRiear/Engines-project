using UnityEngine;

public class Cross : MonoBehaviour {
    
    public GameObject interactionText;
    [SerializeField]private PlayerStats playerStats;
    private CharacterManager manager;

   
    private void Start()
    {
        manager = FindObjectOfType<CharacterManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        interactionText.SetActive(collision.gameObject == manager.player);

        if (collision.gameObject == manager.player && Input.GetKeyDown(KeyCode.E))
        {
            Player();

            Enemies();
        }
    }

    private void Enemies()
    {
        foreach (GameObject enemy in manager.enemies)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            if (!enemyStats.isAlive)
                enemyStats.healthPoints = enemyStats.maxHealthPoints;
        }
    }

    private void Player()
    {
        playerStats.healthPoints = playerStats.maxHealthPoints;
    }
}
