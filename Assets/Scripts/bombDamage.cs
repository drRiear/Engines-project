using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class bombDamage : MonoBehaviour {
    
    [SerializeField] private PlayerStats playerStats;
    private EnemyStats myStats;
    private SpriteRenderer render;
    private CircleCollider2D colider;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
            playerStats.healthPoints--;
        
    }

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        myStats = GetComponent<EnemyStats>();
        colider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        Death();

        if (myStats.isAlive)
        {
            render.color = Color.white;
            colider.enabled = true;
        }
    }

    private void Death()
    {
        if (myStats.healthPoints <= 0)
        {
            colider.enabled = false;
            render.color = Color.black;
            myStats.isAlive = false;
        }
    }
    
}
