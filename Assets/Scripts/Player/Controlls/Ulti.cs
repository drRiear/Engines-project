using UnityEngine;

namespace Player.Controlls
{
    class Ulti : MonoBehaviour
    {
        #region Private Variables
        private Stats myStats;
        public float ultiTimer;
        #endregion

        #region Inspector Variables
        [SerializeField] KeyCode ultiKey;
        #endregion


        #region Unity Events

        private void Start()
        {
            myStats = GetComponent<Stats>();
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
                case Stats.UltiState.Ready:
                    Ready();
                    break;
                case Stats.UltiState.Ulting:
                    Ulting();
                    break;
                case Stats.UltiState.Charging:
                    Charging();
                    break;
            }
        }
        private void Ready()
        {
            if (Input.GetKey(ultiKey) && myStats.canIUlti)
                myStats.ultiState = Stats.UltiState.Ulting;
        }

        private void Ulting()
        {
            ultiTimer -= Time.deltaTime;

            UltiStart();

            if(myStats.ultiPoints > 0.0f)
                myStats.ultiPoints -= myStats.ultiCost * Time.deltaTime;
            else if (myStats.ultiPoints == 0.0f)
                myStats.ultiPoints = 0.0f;


            if (ultiTimer <= 0)
            {
                UltiEnd();
                myStats.ultiState = Stats.UltiState.Charging;
            }
        }
        private void Charging()
        {
            ultiTimer = myStats.ultiDuration;
            if (myStats.canIUlti)
                myStats.ultiState = Stats.UltiState.Ready;
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
}
