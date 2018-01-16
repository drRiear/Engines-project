using UnityEngine;
using UnityEngine.EventSystems;

public class StatButtonsController : MonoBehaviour
{
    public void StrengthOnClick()
    {
        MessageDispatcher.Send(new Messages.PlayerLevelUp(PlayerStats.MainStats.strength));
    }
    public void DexterityOnClick()
    {
        MessageDispatcher.Send(new Messages.PlayerLevelUp(PlayerStats.MainStats.dexterity));
    }
    public void IntelligenceOnClick()
    {
        MessageDispatcher.Send(new Messages.PlayerLevelUp(PlayerStats.MainStats.intelligence));
    }

}
