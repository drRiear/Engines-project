using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayStats : MonoBehaviour {

    public Text healthText;
    public Text staminaText;
    [SerializeField] private PlayerStats playerStats; 

    
	void Start ()
    {
        healthText.text = "Health: " + playerStats.healthPoints;
        staminaText.text = "Stamina: " + playerStats.staminaPoints;
    }
	
	void Update ()
    {
        healthText.text = "Health: " + playerStats.healthPoints;
        staminaText.text = "Stamina: " + Mathf.RoundToInt(playerStats.staminaPoints);
    }
}
