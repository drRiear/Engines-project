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
    [Header("Coins")]
    public Text coinText;
    #endregion

    #region Private Variables
    RectTransform sliderRT;
    private PlayerStats playerStats;
    private float coinCount;
    #endregion

    #region Unity Events
    void Start ()
    {
        MessageDispatcher.AddListener(this);
        playerStats = CharacterManager.Instance.player.GetComponent<PlayerStats>();

        SetUpStaminaBar();
        SetUpHealthBar();
        SetUpUltiBar();
        SetUpCoinText();
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
    private void UpdateCoinText(Messages.CoinPicketUp msg)
    {
        SetUpCoinText();
    }
    private void UpdateCoinText(Messages.PlayerDead msg)
    {
        SetUpCoinText();
    }
    #region SetUp Methods
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
        coinCount = CharacterManager.Instance.player.GetComponent<PlayerInventoryManager>().inventory.coins;
        if (coinCount == 0.0f)
            coinText.transform.parent.gameObject.SetActive(false);
        else
        {
            coinText.transform.parent.gameObject.SetActive(true);
            coinText.text = coinCount.ToString();
        }
    }
    #endregion
    #endregion
}
