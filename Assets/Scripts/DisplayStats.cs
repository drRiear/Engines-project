using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayStats : MonoBehaviour {

    public Slider ultiBar;
    public Slider healthBar;
    public Slider staminaBar;
    RectTransform sliderRT;
    [SerializeField] private PlayerStats playerStats; 

    
	void Start ()
    {
        playerStats = CharacterManager.Instance.player.GetComponent<PlayerStats>();

        SetUpStaminaBar();
        SetUpHealthBar();
        SetUpUltiBar();
    }

    void Update ()
    {
        UpdateBars();
        
    }

    private void UpdateBars()
    {
        staminaBar.value = playerStats.staminaPoints;
        healthBar.value = playerStats.healthPoints;
        ultiBar.value = playerStats.ultiPoints;
    }

    public void SetUpStaminaBar()
    {
        sliderRT = staminaBar.GetComponent<RectTransform>();
        Vector2 sizeDelta = new Vector2(playerStats.maxStaminaPoints * 2, sliderRT.sizeDelta.y);
        sliderRT.sizeDelta = sizeDelta;
        staminaBar.maxValue = playerStats.maxStaminaPoints;
        staminaBar.value = playerStats.staminaPoints;
    }
    public void SetUpHealthBar()
    {
        sliderRT = healthBar.GetComponent<RectTransform>();
        Vector2 sizeDelta = new Vector2(playerStats.maxHealthPoints * 50, sliderRT.sizeDelta.y); // 1HP - 50px(?)
        sliderRT.sizeDelta = sizeDelta;
        healthBar.maxValue = playerStats.maxHealthPoints;
        healthBar.value = playerStats.healthPoints;
    }
    public void SetUpUltiBar()
    {
        ultiBar.maxValue = playerStats.ultiCost;
        ultiBar.value = playerStats.ultiPoints;
    }
}
