using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {


    #region Variables

    [Header("HP/Stamina")]
    public float maxHealthPoints;
    [HideInInspector] public float healthPoints;
    public float maxStaminaPoints;
    [HideInInspector] public float staminaPoints;
    [HideInInspector] public float staminaExpense;
    [HideInInspector] public float staminaRegen;

    [Header("Damage")]
    public float baseDamage;
    public float damage;

    [Header("Movement")]
    public float maxRunSpeed;
    public float runSpeed;
    public float sprintMultiplier;
    public float jumpHeight;

    [Header("Dash")]
    public float maxDashDistance;
    public float dashCooldown;
    public float dashDuration;
    [HideInInspector] public DashState dashState;

    [Header("Ultimate")]
    public float ultiCost;
    [HideInInspector] public float ultiPoints;
    public float ultiDuration;
    public float ultiDamage;
    [HideInInspector] public bool CanIUlti { get { return ultiPoints == ultiCost; } }
    [HideInInspector] public UltiState ultiState;

    [HideInInspector] public bool inAttack;
    [HideInInspector] public bool inSprint;
    [HideInInspector] public bool inDash;
    [HideInInspector] public bool onGround;
    [HideInInspector] public bool isAlive { get { return healthPoints > 0; } }

    //Non-Player vars
    [Header("Other")]
    public DisplayStats displayStats;
    private CharacterManager manager;
    #endregion

    void Awake()
    {
        maxHealthPoints = 5.0f;
        healthPoints = maxHealthPoints;

        maxStaminaPoints = 100.0f;
        staminaPoints = maxStaminaPoints;
        staminaExpense = 5.0f;
        staminaRegen = 10.0f;

        damage = 1.0f;
        baseDamage = damage;

        maxRunSpeed = 10.0f;
        runSpeed = maxRunSpeed;
        sprintMultiplier = 2.0f;
        jumpHeight = 2.0f;

        maxDashDistance = 5f;
        dashCooldown = 1.0f;
        dashDuration = 0.15f;
        dashState = 0;

        ultiCost = 25.0f;
        ultiPoints = 0.0f;
        ultiDuration = 3.0f;
        ultiDamage = 3.0f;
        ultiState = 0;

        manager = FindObjectOfType<CharacterManager>();
        manager.player = gameObject;
    }
    
    public void IncreaseStamina(float newMaxStamina)
    {
        if (newMaxStamina == maxStaminaPoints)
            return;

        maxStaminaPoints = newMaxStamina;
        displayStats.SetUpStaminaBar();
    }
    
    

    #region Enumerations
    public enum DashState { ready, dashing, onCooldown };
    public enum UltiState { charging, ready, ulting };
    #endregion
}
