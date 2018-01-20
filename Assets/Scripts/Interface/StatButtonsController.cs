using Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatButtonsController : MonoBehaviour
{
    public void StrengthOnClick()
    {
        MessageDispatcher.Send(new Messages.PlayerLevelUp(Stats.MainStats.strength));
    }
    public void DexterityOnClick()
    {
        MessageDispatcher.Send(new Messages.PlayerLevelUp(Stats.MainStats.dexterity));
    }
    public void IntelligenceOnClick()
    {
        MessageDispatcher.Send(new Messages.PlayerLevelUp(Stats.MainStats.intelligence));
    }

}
