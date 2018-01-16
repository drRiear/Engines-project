using UnityEngine;
using UnityEngine.UI;

public class PlayerWindowStatsDisplayer : MonoBehaviour
{
    private PlayerStats playerStats;

    #region Inspector Variables
    [Header("Main Stats")]
    [SerializeField] private Text strengthText;
    [SerializeField] private Text dexterityText;
    [SerializeField] private Text intelligenceText;
    #endregion

    #region Unity Events
    private void Start()
    {
        playerStats = CharacterManager.Instance.player.GetComponent<PlayerStats>();
        MessageDispatcher.AddListener(this);
        SetUpAll();
    }
    #endregion

    #region Private Region

    private void SetUpAll()
    {
        SetUpStrength();
        SetUpDexterity();
        SetUpIntelligence();
    }
    private void SetUpStrength()
    {
        strengthText.text = playerStats.strength.ToString();
    }
    private void SetUpDexterity()
    {
        dexterityText.text = playerStats.dexterity.ToString();
    }
    private void SetUpIntelligence()
    {
        intelligenceText.text = playerStats.intelligence.ToString();
    }
    #endregion

    #region Message Based Method
    private void LevelUp(Messages.PlayerLevelUp message)
    {
        switch (message.stat)
        {
            case PlayerStats.MainStats.strength:
                SetUpStrength();
                break;
            case PlayerStats.MainStats.dexterity:
                SetUpDexterity();
                break;
            case PlayerStats.MainStats.intelligence:
                SetUpIntelligence();
                break;
        }
    }
    #endregion


}