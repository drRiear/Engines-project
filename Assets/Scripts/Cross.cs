using UnityEngine;

public class Cross : MonoBehaviour {
    
    private Rect textRect;
    private GUIStyle skin = new GUIStyle();
    private PlayerStats playerStats;
    private bool playerOnTrigger;

    #region Unity Events
    private void Start()
    {
        EventManager.StartListening("Interaction", Inter);

        playerStats = CharacterManager.Instance.player.GetComponent<PlayerStats>();

        textRect = new Rect(Screen.width / 2.0f, Screen.height / 5.0f, 10.0f, 100.0f);
        skin.fontSize = 40;
        skin.alignment = TextAnchor.MiddleCenter;
    }
    private void OnGUI()
    {
        if (playerOnTrigger)
            GUI.Label(textRect, "Press E to interact", skin);
    }
    #endregion


    private void OnTriggerStay2D(Collider2D collision)
    {
        playerOnTrigger = collision.gameObject == CharacterManager.Instance.player.gameObject;
    }

    #region Private Methods
    private void Inter()
    {
        if (!playerOnTrigger) return;

        print("Cross Interaction");
        Player();
        Enemies();
    }
    private void Enemies()
    {
        foreach (GameObject enemy in CharacterManager.Instance.enemiesList)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            if (!enemyStats.isAlive)
                enemyStats.healthPoints = enemyStats.maxHealthPoints;
        }
    }

    private void Player()
    {
        playerStats.healthPoints = playerStats.maxHealthPoints;
    }
    #endregion
}
