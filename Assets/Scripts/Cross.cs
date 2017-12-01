using UnityEngine;

public class Cross : MonoBehaviour {
    
    private Rect textRect;
    private GUIStyle skin = new GUIStyle();
    private PlayerStats playerStats;
    private bool drawText;

    private void Start()
    {
        playerStats = CharacterManager.Instance.player.GetComponent<PlayerStats>();

        textRect = new Rect(Screen.width / 2.0f, Screen.height / 5.0f, 10.0f, 100.0f);
        skin.fontSize = 40;
        skin.alignment = TextAnchor.MiddleCenter;
    }
    private void OnGUI()
    {
        if (drawText)
            GUI.Label(textRect, "Press E to interact", skin);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CharacterManager.Instance == null)
            return;
        drawText = collision.gameObject == CharacterManager.Instance.player.gameObject;
        
        if (collision.gameObject == CharacterManager.Instance.player.gameObject && Input.GetKeyDown(KeyCode.E))
        {
            Player();

            Enemies();
        }
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
}
