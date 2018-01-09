using UnityEngine;

public class ThornStats : MonoBehaviour
{
    #region Variables
    public float maxHealthPoints;
    [HideInInspector] public float healthPoints;
    public float maxSpeed;
    [HideInInspector] public float speed;
    public float damage;
    [HideInInspector] public bool isAlive { get { return healthPoints > 0; } }
    public float souls;
    #endregion

    #region Unity Events
    private void Awake()
    {
        MessageDispatcher.AddListener(this);

        healthPoints = maxHealthPoints;
        speed = maxSpeed;
        souls = 10.0f;

        AddToCharacterManager();
    }

    private void AddToCharacterManager()
    {
        var list = CharacterManager.Instance.thornsList;
        if (list.Contains(gameObject))
            return;
        else
            list.Add(gameObject);
    }
    #endregion

    #region Private Methods
    private void Hurted (Messages.EnemyHurted message)
    {
        if (message.enemy != gameObject)
            return;

        healthPoints -= message.damage;

        if (!isAlive)
            MessageDispatcher.Send(new Messages.EnemyDead(gameObject, souls));
    }
    private void Death(Messages.EnemyDead message)
    {
        if (message.enemy != gameObject)
            return;
        gameObject.SetActive(false);
    }
    private void CrossUsed(Messages.Cross message)
    {
        if (isAlive)
            return;

        healthPoints = maxHealthPoints;
        gameObject.SetActive(true);
    }
    #endregion
} 