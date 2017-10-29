using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {


    #region Variables
    public float maxHealthPoints;
    [HideInInspector] public float healthPoints;
    public float maxStaminaPoints;
    [HideInInspector] public float staminaPoints;
    [HideInInspector] public float staminaExpense;
    [HideInInspector] public float staminaRegen;
    public float maxRunSpeed;
    public float runSpeed;
    public float sprintMultiplier;
    public float jumpHeight;
    public float maxDashDistance;
    public float dashCooldown;
    public float dashDuration;
    [HideInInspector] public bool inAttack;
    [HideInInspector] public bool inSprint;
    [HideInInspector] public bool inDash;
    [HideInInspector] public bool onGround;

    #endregion

    void Awake()
    {
        maxHealthPoints = 5f;
        healthPoints = maxHealthPoints;

        maxStaminaPoints = 100f;
        staminaPoints = maxStaminaPoints;
        staminaExpense = 5f;
        staminaRegen = 10f;

        maxRunSpeed = 10f;
        runSpeed = maxRunSpeed;
        sprintMultiplier = 2f;
        jumpHeight = 2f;

        maxDashDistance = 5f;
        dashCooldown = 1.0f;
        dashDuration = 0.15f;
    }
}
