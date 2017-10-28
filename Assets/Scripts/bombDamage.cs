using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class bombDamage : MonoBehaviour {
    
    [SerializeField] private PlayerStats playerStats;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
            playerStats.healthPoints--;
        
    }
    
}
