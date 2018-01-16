using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] private GameObject statsWindow;
    [Header("Buttons")]
    [SerializeField] private Button strengthButton;
    [SerializeField] private Button dexterityButton;
    [SerializeField] private Button intelligenceButton;
    #endregion

    #region Private Variables
    private bool statsWindowOpened = false;
    private PlayerStats playerStats;
    #endregion
    
    #region Unity Events
    private void Start()
    {
        MessageDispatcher.AddListener(this);
        playerStats = CharacterManager.Instance.player.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (statsWindow.gameObject.activeInHierarchy == false) return;

        ButtonsSetActive(playerStats.canLevelUp == true);
    }
    #endregion

    #region Private Methods
    private void ButtonsSetActive(bool toogle)
    {
        strengthButton.gameObject.SetActive(toogle);
        dexterityButton.gameObject.SetActive(toogle);
        intelligenceButton.gameObject.SetActive(toogle);
    }
    #endregion

    #region Messaga Based Methods
    private void StatsWindow(Messages.StatsWindowOpened message)
    {
        statsWindowOpened = !statsWindowOpened;
        statsWindow.SetActive(statsWindowOpened);
    }
    #endregion  
}
