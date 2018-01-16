using UnityEngine;

class PlayerUltiController : MonoBehaviour
{
    #region Private Variables
    private PlayerStats myStats;
    public float ultiTimer;
    #endregion

    #region Inspector Variables
    [SerializeField] KeyCode ultiKey;
    #endregion


    #region Unity Events

    private void Start()
    {
        myStats = GetComponent<PlayerStats>();
        ultiTimer = myStats.ultiDuration;
    }
    private void Update()
    {
        if (myStats.canIControll)
            UltiMechanic();
    }
    #endregion
    
    #region Private Methods
    private void UltiMechanic()
    {
        switch (myStats.ultiState)
        {
            case PlayerStats.UltiState.Ready:
                Ready();
                break;
            case PlayerStats.UltiState.Ulting:
                Ulting();
                break;
            case PlayerStats.UltiState.Charging:
                Charging();
                break;
        }
    }
    private void Ready()
    {
        if (Input.GetKey(ultiKey) && myStats.canIUlti)
            myStats.ultiState = PlayerStats.UltiState.Ulting;
    }

    private void Ulting()
    {
        ultiTimer -= Time.deltaTime;

        UltiStart();
        myStats.ultiPoints -= myStats.ultiCost * Time.deltaTime;


        if (ultiTimer <= 0)
        {
            UltiEnd();
            myStats.ultiState = PlayerStats.UltiState.Charging;
        }
    }
    private void Charging()
    {
        ultiTimer = myStats.ultiDuration;
        if (myStats.canIUlti)
            myStats.ultiState = PlayerStats.UltiState.Ready;
    }

    private void UltiStart()
    {
        myStats.damage = myStats.ultiDamage;
    }

    private void UltiEnd()
    {
        myStats.damage = myStats.baseDamage;
    }

    #endregion  
}
