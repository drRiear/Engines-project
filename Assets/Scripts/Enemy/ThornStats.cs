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
    #endregion

    #region Unity Events
    private void Awake()
    {
        healthPoints = maxHealthPoints;
        speed = maxSpeed;

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

    private void Update()
    {
        if (!isAlive)
            Death();
    }
    #endregion

    #region Private Methods
    private void Death()
    {
        CharacterManager.Instance.enemiesList.Remove(gameObject);
        Destroy(gameObject);
    }
    #endregion
}
