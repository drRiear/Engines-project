using UnityEngine;

public class Cross : MonoBehaviour {
    
    private Rect textRect;
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
       
        MessageDispatcher.Send(new Messages.Cross());
    }
    private void Reviving(Messages.PlayerRevived message)
    {
        MessageDispatcher.Send(new Messages.Cross());
    }
    #endregion
}
