using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWindowStatsDisplayer : MonoBehaviour
{
    private Stats playerStats;

    #region Inspector Variables
    [Header("Bars")]
    [SerializeField] private Text healthBarText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Text staminaBarText;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Text ultiBarText;
    [SerializeField] private Slider ultiBar;

    [Header("Main Stats")]
    [SerializeField] private Text strengthText;
    [SerializeField] private Text dexterityText;
    [SerializeField] private Text intelligenceText;

    [Header("Level")]
    [SerializeField] private Text levelText;
    [SerializeField] private Text soulsToLevelText;

    [Header("Additional stats")]

    [Header("Stamina stats")]
    [SerializeField] private Text maxHPText;
    [SerializeField] private Text damageText;

    [Header("Desterity stats")]
    [SerializeField] private Text maxStaminaText;
    [SerializeField] private Text staminaRegenText;
    [SerializeField] private Text attackSpeedText;
    [SerializeField] private Text runSpeedText;

    [Header("Intelligence stats")]
    [SerializeField] private Text ultiCostText;
    [SerializeField] private Text ultiChargeSpeedText;
    #endregion

    #region Unity Events
    private void Start()
    {
        playerStats = CharacterManager.Instance.player.GetComponent<Stats>();
        MessageDispatcher.AddListener(this);
        SetUpAll();
    }
    private void Update()
    {
        UpdateHealthBar();
        UpdateStaminaBar();
        UpdateUltiBar();
    }
    #endregion

    #region Private Region

    private void SetUpAll()
    {
        SetUpBarsMaxValue();
        UpdateHealthBar();
        UpdateStaminaBar();
        UpdateUltiBar();

        UpdateStrength();
        UpdateDexterity();
        UpdateIntelligence();
        LevelUp();
    }

    private void SetUpBarsMaxValue()
    {
        healthBar.maxValue = playerStats.maxHealthPoints;
        staminaBar.maxValue = playerStats.maxStaminaPoints;
        ultiBar.maxValue = playerStats.ultiCost;
    }

    private void UpdateHealthBar()
    {
        healthBar.value = playerStats.healthPoints;
        healthBarText.text = (int)playerStats.healthPoints + " / " + playerStats.maxHealthPoints;
    }
    private void UpdateStaminaBar()
    {
        staminaBar.value = playerStats.staminaPoints;
        staminaBarText.text = (int)playerStats.staminaPoints + " / " + playerStats.maxStaminaPoints;
    }
    private void UpdateUltiBar()
    {
        ultiBar.value = playerStats.ultiPoints;
        ultiBarText.text = (int)playerStats.ultiPoints + " / " + playerStats.ultiCost;
    }
    private void UpdateStrength()
    {
        SetUpBarsMaxValue();
        strengthText.text = playerStats.strength.ToString();
        maxHPText.text = playerStats.maxHealthPoints.ToString();
        damageText.text = playerStats.damage.ToString();
    }
    private void UpdateDexterity()
    {
        SetUpBarsMaxValue();
        dexterityText.text = playerStats.dexterity.ToString();
        maxStaminaText.text = playerStats.maxStaminaPoints.ToString();
        staminaRegenText.text = playerStats.staminaRegen.ToString();
        attackSpeedText.text = playerStats.attackSpeed.ToString();
        runSpeedText.text = playerStats.maxRunSpeed.ToString();
    }
    private void UpdateIntelligence()
    {
        SetUpBarsMaxValue();
        intelligenceText.text = playerStats.intelligence.ToString();
        ultiCostText.text = playerStats.ultiCost.ToString();
        ultiChargeSpeedText.text = playerStats.damageToUltiPoints.ToString();
    }
    private void LevelUp()
    {
        levelText.text = playerStats.level.ToString();
        soulsToLevelText.text = playerStats.soulsToNextLevel.ToString();
    }
    #endregion

    #region Message Based Method
    private void LevelUp(Messages.Player.LevelUp message)
    {
        LevelUp();
        switch (message.stat)
        {
            case Stats.MainStats.strength:
                UpdateStrength();
                break;
            case Stats.MainStats.dexterity:
                UpdateDexterity();
                break;
            case Stats.MainStats.intelligence:
                UpdateIntelligence();
                break;
        }
    }
    #endregion


}