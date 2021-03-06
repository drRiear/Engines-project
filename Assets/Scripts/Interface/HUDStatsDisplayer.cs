﻿using Player;
using Player.Inventory;
using UnityEngine.UI;
using UnityEngine;

public class HUDStatsDisplayer : MonoBehaviour
{
    #region Public Variables
    [Header("Stats")]
    public Slider ultiBar;
    public Slider healthBar;
    public Slider staminaBar;
    [Header("Inventory")]
    public Text coinText;
    public Text soulsText;
    #endregion

    #region Private Variables
    RectTransform sliderRT;
    private Stats playerStats;
    private Inventory inventory;
    #endregion

    #region Unity Events
    void Start ()
    {
        MessageDispatcher.AddListener(this);
        playerStats = CharacterManager.Instance.player.GetComponent<Stats>();
        inventory = CharacterManager.Instance.player.GetComponent<InventoryManager>().inventory;
        

        SetUpAll();
    }

    void Update ()
    {
        UpdateBars();
    }
    #endregion

    #region Private Methods
    private void UpdateBars()
    {
        staminaBar.value = playerStats.staminaPoints;
        healthBar.value = playerStats.healthPoints;
        ultiBar.value = playerStats.ultiPoints;
    }
    private void UpdateCoinText(Messages.CoinPicketUp message)
    {
        SetUpCoinText();
    }
    private void UpdateSoulText(Messages.SoulsPicketUp message)
    {
        SetUpSoulsText();
    }
    private void UpdateSoulText(Messages.Player.LevelUp message)
    {
        SetUpSoulsText();
    }
    private void Death(Messages.Player.Dead message)
    {
        SetUpCoinText();
        SetUpSoulsText();
    }

    #region SetUp Methods
    private void SetUpAll()
    {
        SetUpStaminaBar();
        SetUpHealthBar();
        SetUpUltiBar();
        SetUpCoinText();
        SetUpSoulsText();
    }
    private void SetUpStaminaBar()
    {
        sliderRT = staminaBar.GetComponent<RectTransform>();
        Vector2 sizeDelta = new Vector2(playerStats.maxStaminaPoints * 2, sliderRT.sizeDelta.y);
        sliderRT.sizeDelta = sizeDelta;
        staminaBar.maxValue = playerStats.maxStaminaPoints;
        staminaBar.value = playerStats.staminaPoints;
    }
    private void SetUpHealthBar()
    {
        sliderRT = healthBar.GetComponent<RectTransform>();
        Vector2 sizeDelta = new Vector2(playerStats.maxHealthPoints * 50, sliderRT.sizeDelta.y); // 1HP - 50px(?)
        sliderRT.sizeDelta = sizeDelta;
        healthBar.maxValue = playerStats.maxHealthPoints;
        healthBar.value = playerStats.healthPoints;
    }
    private void SetUpUltiBar()
    {
        ultiBar.maxValue = playerStats.ultiCost;
        ultiBar.value = playerStats.ultiPoints;
    }
    private void SetUpCoinText()
    {
        if (inventory.coins == 0.0f)
            coinText.transform.parent.gameObject.SetActive(false);
        else
        {
            coinText.transform.parent.gameObject.SetActive(true);
            coinText.text = inventory.coins.ToString();
        }
    }
    private void SetUpSoulsText()
    {
        if (inventory.souls == 0.0f)
            soulsText.transform.parent.gameObject.SetActive(false);
        else
        {
            soulsText.transform.parent.gameObject.SetActive(true);
            soulsText.text = inventory.souls.ToString();
        }
    }
    #endregion
    #endregion

    private void LevelUp(Messages.Player.LevelUp message)
    {
        switch (message.stat)
        {
            case Stats.MainStats.strength:
                SetUpHealthBar();
                break;
            case Stats.MainStats.dexterity:
                SetUpStaminaBar();
                break;
        }
    }
}
