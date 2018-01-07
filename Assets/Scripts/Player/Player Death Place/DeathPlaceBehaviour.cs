using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaceBehaviour : MonoBehaviour
{
    private Rect textRect;
    private GUIStyle skin = new GUIStyle();
    private bool playerOnTrigger = false;
    private bool playerIsAlive = false;
    private float coins;

    #region Unity Events
    private void Start()
    {
        MessageDispatcher.AddListener(this);

        coins = CharacterManager.Instance.player.GetComponent<PlayerInventoryManager>().dropedCoins;

        SetUpGUISkin();
    }
    private void OnGUI()
    {
        if (playerOnTrigger && playerIsAlive)
            GUI.Label(textRect, "Press E to interact", skin);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == CharacterManager.Instance.player)
            playerOnTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == CharacterManager.Instance.player)
            playerOnTrigger = false;
    }
    #endregion

    #region Private Methods
    private void SetUpGUISkin()
    {
        textRect = new Rect(Screen.width / 2.0f, Screen.height / 5.0f, 10.0f, 100.0f);
        skin.fontSize = 40;
        skin.alignment = TextAnchor.MiddleCenter;
    }
    private void Interaction(Messages.Interaction message)
    {
        if (!playerOnTrigger) return;
        MessageDispatcher.Send(new Messages.CoinPicketUp(coins));
        Destroy(gameObject);
    }
    private void PlayerRevived(Messages.PlayerRevived msg)
    {
        playerIsAlive = true;
    }
    private void PlayerDead(Messages.PlayerDead msd)
    {
        Destroy(gameObject);
    }
    #endregion
}
