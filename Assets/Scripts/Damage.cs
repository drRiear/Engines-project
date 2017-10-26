using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Damage : MonoBehaviour {

    #region Variables
    private int Health;
    private int maxHealth = 5;
    private Rigidbody2D rb;
    public Text healthText;

    #endregion

    // Use this for initialization
    void Start () {
        healthText.text = "Health: " + Health;
        rb = GetComponent<Rigidbody2D>();
        Health = maxHealth;
        
	}
	
	// Update is called once per frame
	void Update () {
        healthText.text = "Health: " + Health;

        if (Health == 0)
            Death();
    }

    private void Death()
    {
        Time.timeScale = 0;
        healthText.text = "Dead";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Health--;
        
    }
    
}
