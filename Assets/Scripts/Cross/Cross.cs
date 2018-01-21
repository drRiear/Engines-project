using UnityEngine;

public class Cross : MonoBehaviour {

    private bool playerOnTrigger = false;

    #region Unity Events
    private void Start()
    {
        MessageDispatcher.AddListener(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == CharacterManager.Instance.player)
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
       
        MessageDispatcher.Send(new Messages.Cross());
    }
    private void Reviving(Messages.Player.Revived message)
    {
        MessageDispatcher.Send(new Messages.Cross());
    }
    #endregion
}
