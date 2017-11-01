using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour {
    
    public GameObject interactionText;
    [SerializeField]private PlayerStats playerStats;
    private GameObject[] enemies;

   
    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
            interactionText.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
            interactionText.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero" && Input.GetKeyDown(KeyCode.E))
        {
            playerStats.healthPoints = playerStats.maxHealthPoints;

            foreach (GameObject enemy in enemies)
            {
                EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
                if (!enemyStats.isAlive)
                {
                    enemyStats.isAlive = true;
                    enemyStats.healthPoints = enemyStats.maxHealthPoints;
                }
            }
        }
    }
}
