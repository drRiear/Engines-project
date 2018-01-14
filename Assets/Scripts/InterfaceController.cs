using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] private GameObject statsWindow;
    #endregion
    
    #region Private Variables
    private bool statsWindowOpened = false;
    #endregion
    
    #region Unity Events
    private void Start()
    {
        MessageDispatcher.AddListener(this);
    }
    #endregion
    
    #region Private Methods
    private void StatsWindow(Messages.StatsWindowOpened message)
    {
        statsWindowOpened = !statsWindowOpened;
        statsWindow.SetActive(statsWindowOpened);
    }
    #endregion
}
