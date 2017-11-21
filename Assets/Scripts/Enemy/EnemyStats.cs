using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {


    #region Variables
    public float maxHealthPoints;
    [HideInInspector] public float healthPoints;  
    public float maxRunSpeed;
    [HideInInspector] public float runSpeed;
    public float damage;
    [HideInInspector] public bool isAlive { get { return healthPoints > 0; } }

    [HideInInspector] public CharacterManager manager;
    #endregion

    void Awake ()
    {
        healthPoints = maxHealthPoints;
        runSpeed = maxRunSpeed;

        manager = FindObjectOfType<CharacterManager>();
        manager.enemies.Add(gameObject);
    }
}
