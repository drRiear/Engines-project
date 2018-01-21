using Player.Inventory;
using UnityEngine;

public class DeathPlaceBehaviour : MonoBehaviour
{
    private bool playerOnTrigger = false;
    private bool playerIsAlive = false;
    private Inventory inventory;

    #region Unity Events
    private void Start()
    {
        MessageDispatcher.AddListener(this);

        inventory = CharacterManager.Instance.player.GetComponent<InventoryManager>().dropedInventory;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == CharacterManager.Instance.player && playerIsAlive)
        {
            playerOnTrigger = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == CharacterManager.Instance.player)
        {
            playerOnTrigger = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    #endregion

    #region Private Methods
    private void Interaction(Messages.Interaction message)
    {
        if (!playerOnTrigger) return;
        MessageDispatcher.Send(new Messages.CoinPicketUp(inventory.coins));
        MessageDispatcher.Send(new Messages.SoulsPicketUp(inventory.souls));
        Destroy(gameObject);
    }
    private void PlayerRevived(Messages.Player.Revived message)
    {
        playerIsAlive = true;
    }
    private void PlayerDead(Messages.Player.Dead message)
    {
        Destroy(gameObject);
    }
    #endregion
}
