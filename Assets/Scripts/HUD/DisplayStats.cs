using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayStats : MonoBehaviour {
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
    private PlayerStats playerStats;
    private Inventory inventory;
    #endregion

    #region Unity Events
    void Start ()
    {
        MessageDispatcher.AddListener(this);
        playerStats = CharacterManager.Instance.player.GetComponent<PlayerStats>();
        inventory = CharacterManager.Instance.player.GetComponent<PlayerInventoryManager>().inventory;

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
    private void Death(Messages.PlayerDead message)
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
}
