using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaceBehaviour : MonoBehaviour
{
    private Rect textRect;
    private bool playerOnTrigger = false;
    private bool playerIsAlive = false;
    private float coins;

    #region Unity Events
    private void Start()
    {
        MessageDispatcher.AddListener(this);

        coins = CharacterManager.Instance.player.GetComponent<PlayerInventoryManager>().dropedCoins;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == CharacterManager.Instance.player && playerIsAlive)
        {
            playerOnTrigger = true;
            GameObject go = transform.GetChild(0).gameObject;
            go.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == CharacterManager.Instance.player)
        {
            playerOnTrigger = false;
            GameObject go = transform.GetChild(0).gameObject;
            go.SetActive(false);
        }
    }
    #endregion

    #region Private Methods
    private void Interaction(Messages.Interaction message)
    {
        if (!playerOnTrigger) return;
        MessageDispatcher.Send(new Messages.CoinPicketUp(coins));
        Destroy(gameObject);
    }
    private void PlayerRevived(Messages.PlayerRevived message)
    {
        playerIsAlive = true;
    }
    private void PlayerDead(Messages.PlayerDead message)
    {
        Destroy(gameObject);
    }
    #endregion
}
