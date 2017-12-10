using UnityEngine;

public class Cross : MonoBehaviour {
    
    private Rect textRect;
    private GUIStyle skin = new GUIStyle();
    private bool playerOnTrigger = false;

    #region Unity Events
    private void Start()
    {
        MessageDispatcher.AddListener(this);

        textRect = new Rect(Screen.width / 2.0f, Screen.height / 5.0f, 10.0f, 100.0f);
        skin.fontSize = 40;
        skin.alignment = TextAnchor.MiddleCenter;
    }
    private void OnGUI()
    {
        if (playerOnTrigger)
            GUI.Label(textRect, "Press E to interact", skin);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == CharacterManager.Instance.player)
            playerOnTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == CharacterManager.Instance.player)
            playerOnTrigger = false;
    }
    #endregion

    #region Private Methods
    private void Interaction(Messages.Interaction message)
    {
        if (!playerOnTrigger) return;
       
        MessageDispatcher.Send(new Messages.Cross());
    }
    #endregion
}
