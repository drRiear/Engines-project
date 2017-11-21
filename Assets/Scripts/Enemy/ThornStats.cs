using UnityEngine;

public class ThornStats : EnemyStats
{


    #region Variables
    #endregion

    void Awake ()
    {
        healthPoints = maxHealthPoints;
        runSpeed = maxRunSpeed;
        
        manager = FindObjectOfType<CharacterManager>();
        manager.thorns.Add(gameObject);
    }
}
