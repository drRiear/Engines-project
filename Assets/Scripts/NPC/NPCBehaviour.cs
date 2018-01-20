using UnityEngine;
using System.Collections;

public class NPCBehaviour : MonoBehaviour
{
    public Canvas interactionCanvas;

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
            interactionCanvas.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == CharacterManager.Instance.player)
        {
            playerOnTrigger = false;
            interactionCanvas.gameObject.SetActive(false);
            MessageDispatcher.Send(new Messages.DialogueStops(gameObject));
        }
    }
    #endregion

    #region Private Methods
    private void Interaction(Messages.Interaction message)
    {
        if (!playerOnTrigger) return;
        interactionCanvas.gameObject.SetActive(false);
        MessageDispatcher.Send(new Messages.DialogueStart(gameObject));
    }
    #endregion
}
