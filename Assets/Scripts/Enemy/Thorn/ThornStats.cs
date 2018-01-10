﻿using UnityEngine;

public class ThornStats : MonoBehaviour
{
    #region Public Variables
    public bool canRevive = true;
    public float maxHealthPoints;
    public float damage;
    public float souls;
    #endregion

    #region Hiden Variables
    [HideInInspector] public float healthPoints;
    [HideInInspector] public bool isAlive { get { return healthPoints > 0; } }
    #endregion

    #region Unity Events
    private void Awake()
    {
        MessageDispatcher.AddListener(this);

        healthPoints = maxHealthPoints;

        AddToCharacterManager();
    }
    #endregion

    #region Private Methods
    private void AddToCharacterManager()
    {
        var list = CharacterManager.Instance.thornsList;
        if (list.Contains(gameObject))
            return;
        else
            list.Add(gameObject);
    }
    #endregion
} 