﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Variables

    #region STR DEX INT
    [Header("Main stats")]
    
    public float strength;
    public float dexterity;
    public float intelligence;
    #endregion  

    #region Leveling
    [Header("Leveling")]
    public float level;
    public float soulsToNextLevel;
    private Inventory myInventory = new Inventory();
    public bool canLevelUp { get { return myInventory.souls >= soulsToNextLevel; } }
    #endregion

    #region HP / Stamina 
    [Header("HP/Stamina")]
    public float maxHealthPoints;
    [HideInInspector] public float healthPoints;
    public float maxStaminaPoints;
    [HideInInspector] public float staminaPoints;
    [HideInInspector] public float staminaUsage;
    [HideInInspector] public float staminaRegen;
    #endregion  

    #region Attack
    [Header("Attack")]
    public float attackDelay;
    public float baseDamage;
    public float damage;
    public float damageToUltiPoints;
    #endregion  

    #region Movement
    [Header("Movement")]
    public float maxRunSpeed;
    public float currentRunSpeed;
    public float sprintMultiplier;
    public float jumpHeight;
    #endregion  

    #region Dash
    [Header("Dash")]
    public float dashMaxDistance;
    public float dashCooldown;
    public float dashCost;
    [HideInInspector] public DashState dashState;
    #endregion  

    #region Ultimate
    [Header("Ultimate")]
    public float ultiCost;
    [HideInInspector] public float ultiPoints;
    public float ultiDuration;
    public float ultiDamage;
    public bool canIUlti { get { return ultiPoints == ultiCost; } }
    [HideInInspector] public UltiState ultiState;
    #endregion  

    #region State Bools
    [HideInInspector] public bool inAttack;
    [HideInInspector] public bool inSprint;
    [HideInInspector] public bool onMove;
    [HideInInspector] public bool onGround;
    [HideInInspector] public bool canIControll = true;
    public bool isAlive { get { return healthPoints > 0; } }
    #endregion

    #region Death
    [HideInInspector] public Vector3 lastCrossPosition = new Vector3();
    #endregion

    #endregion

    private void Awake()
    {
        //Main Stats
        strength = 5.0f;
        dexterity = 5.0f;
        intelligence = 5.0f;

        //Leveling
        soulsToNextLevel = 5.0f;
        myInventory = GetComponent<PlayerInventoryManager>().inventory;

        // HP Stamina
        maxHealthPoints = 5.0f;
        healthPoints = maxHealthPoints;
        maxStaminaPoints = 100.0f;
        staminaPoints = maxStaminaPoints;
        staminaUsage = 5.0f;
        staminaRegen = 10.0f;

        //Attack
        attackDelay = 0.5f;
        damage = 1.0f;
        baseDamage = damage;
        damageToUltiPoints = 0.5f; // %

        //Movement
        maxRunSpeed = 20.0f;
        currentRunSpeed = maxRunSpeed;
        sprintMultiplier = 2.0f;
        jumpHeight = 20.0f;             //+- = rb.velocity.y

        //Dash
        dashMaxDistance = 10.0f;
        dashCooldown = 1.0f;
        dashCost = 20.0f;
        dashState = DashState.Ready;

        //Ulti
        ultiCost = 35.0f;
        ultiPoints = 0.0f;
        ultiDuration = 3.0f;
        ultiDamage = 3.0f;
        ultiState = 0;

        lastCrossPosition = transform.position;

        CharacterManager.Instance.player = gameObject;
        MessageDispatcher.AddListener(this);
    }
    #region Public Methods
    public void IncreaseUltiPoints(float points)
    {
        if (ultiPoints + points > ultiCost)
            ultiPoints = ultiCost;
        else
            ultiPoints += points;
    }
    #endregion

    #region Private Methods
    private void IncreaseLevel()
    {
        level++;
        myInventory.souls -= soulsToNextLevel;

        soulsToNextLevel += 5.0f;
    }
    private void IncreaseStrength()
    {
        strength++;
        maxHealthPoints += 1;
        damage += 1;
    }
    private void IncreaseDexterity()
    {
        dexterity++;
        maxStaminaPoints += 10.0f;
        staminaRegen += 0.5f;
        staminaUsage -= 0.1f;
        attackDelay -= 0.1f;
        maxRunSpeed += 2.0f;
    }
    private void IncreaseIntelligence()
    {
        intelligence++;
        damageToUltiPoints += 0.05f;
        ultiCost -= 2.0f;
    }
    #endregion
    #region Message Based Methods
    private void CrossUsed(Messages.Cross message)
    {
        healthPoints = maxHealthPoints;
        lastCrossPosition = transform.position;
    }
    private void Hurted(Messages.PlayerHurted message)
    {
        healthPoints -= message.damage;

        if (!isAlive)
            MessageDispatcher.Send(new Messages.PlayerDead(transform.position));      //Insert player revive controller
    }
    private void LevelUp(Messages.PlayerLevelUp message)
    {
        switch (message.stat)
        {
            case MainStats.strength:
                IncreaseStrength();
                IncreaseLevel();
                break;
            case MainStats.dexterity:
                IncreaseDexterity();
                IncreaseLevel();
                break;
            case MainStats.intelligence:
                IncreaseIntelligence();
                IncreaseLevel();
                break;
        }
    }
    #endregion

    #region Enumerations
    public enum DashState { Ready, Dashing, Cooldown };
    public enum UltiState { Ready, Ulting, Charging };

    public enum MainStats { strength, dexterity, intelligence }
    #endregion
}
