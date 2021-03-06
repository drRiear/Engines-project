﻿using UnityEngine;

namespace Enemy.Thorn
{
    public class ThornBehaviour : MonoBehaviour {

        #region Private Variables
        private GameObject player;
        private Stats myStats;

        private Vector3 startPosition;
        private bool agred = false;
        #endregion

        #region Inspector Variables
        [SerializeField] private bool drawGizmos;
        #endregion

        #region Public Variables
        public float speed;
        [Header("")]
        public ThornType type = ThornType.nonReturnable;
        public float agrRadius;
        [Tooltip("Only for returnable Thorns")]
        public float visionRadius;
        #endregion

        #region Unity Events
        private void Start()
        {
            myStats = GetComponent<Stats>();
        
            player = CharacterManager.Instance.player;

            startPosition = transform.position;

            MessageDispatcher.AddListener(this);
        }
        private void Update()
        {
            if (type == ThornType.nonReturnable)
                CheckDistance_NR();
            else if (type == ThornType.returnable)
                CheckDistance_R();
        }
        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, agrRadius);

            if (type == ThornType.returnable)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, visionRadius);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
                PlayerCollision();
        }
        #endregion


        #region Private Methods
    
        private void CheckDistance_NR()
        {
            var distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= agrRadius)
                agred = true;
            Move();
        }
        private void CheckDistance_R()
        {
            var distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance <= agrRadius)
                agred = true;
            else if (distance >= visionRadius)
                agred = false;
            Move();
        }
        private void Move()
        {
            if (agred)
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            else if (!agred && transform.position != startPosition)
                transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
        }
        private void PlayerCollision()
        {
            MessageDispatcher.Send(new Messages.Player.Hurted(myStats.damage));

            if (!myStats.canRevive)
                Destroy(gameObject);
            else
            {
                myStats.healthPoints = 0.0f;
                gameObject.SetActive(false);
            }
        }
        #endregion

        #region Message based methods
        private void CrossUsed(Messages.Cross message)
        {
            if (!myStats.isAlive)
            {
                myStats.healthPoints = myStats.maxHealthPoints;
                gameObject.SetActive(true);
            }

            if (agred)
            {
                transform.position = startPosition;
                agred = false;
            }

        }
        private void Hurted(Messages.Enemy.Hurted message)
        {
            if (message.enemy != gameObject)
                return;

            myStats.healthPoints -= message.damage;

            if (!myStats.isAlive)
                Death();
        }
        private void Death()
        {
            MessageDispatcher.Send(new Messages.Enemy.Dead(gameObject)); 
            MessageDispatcher.Send(new Messages.SoulsPicketUp(myStats.souls));

            if (!myStats.canRevive)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }
        #endregion

        public enum ThornType { nonReturnable, returnable };
    }
}