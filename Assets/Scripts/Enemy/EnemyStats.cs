using UnityEngine;

public class EnemyStats : MonoBehaviour {

    #region Public Variables
    public bool canRevive;
    public float maxHealthPoints;
    public float damage;
    public float souls;
    #endregion

    #region Hiden Variables;
    [HideInInspector] public float healthPoints;
    [HideInInspector] public bool isAlive { get { return healthPoints > 0; } }
    #endregion


    #region Unity Events
    private void Awake ()
    {
        healthPoints = maxHealthPoints;

        CharacterManager.Instance.enemiesList.Add(gameObject);
    }
    #endregion
}
