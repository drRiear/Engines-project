using Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatButtonsController : MonoBehaviour
{
    public void StrengthOnClick()
    {
        MessageDispatcher.Send(new Messages.Player.LevelUp(Stats.MainStats.strength));
    }
    public void DexterityOnClick()
    {
        MessageDispatcher.Send(new Messages.Player.LevelUp(Stats.MainStats.dexterity));
    }
    public void IntelligenceOnClick()
    {
        MessageDispatcher.Send(new Messages.Player.LevelUp(Stats.MainStats.intelligence));
    }

}
