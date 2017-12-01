﻿using System.Collections;
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
    #endregion

    #region Unity Events
    private void Awake ()
    {
        healthPoints = maxHealthPoints;
        runSpeed = maxRunSpeed;
    }

    private void Start()
    {
        CharacterManager.Instance.enemiesList.Add(gameObject);
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