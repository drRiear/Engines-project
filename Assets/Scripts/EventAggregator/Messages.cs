using UnityEngine;

public class Messages
{
    public class Interaction
    {
    }
    public class Cross
    {
    }
    public class CoinPicketUp
    {
        public float cost;
        #region Constructors
        public CoinPicketUp()
        {
        }
        public CoinPicketUp(float cost)
        {
            this.cost = cost;
        }
        #endregion
    }

    #region Player Messages
    public class PlayerHurted
    {
        public float damage;
        #region Constructors
        public PlayerHurted()
        {
        }

        public PlayerHurted(float damage)
        {
            this.damage = damage;
        }
        #endregion
    }
    public class PlayerJump
    {
    }
    public class PlayerLanded
    {
    }
    public class PlayerDead
    {
        public Vector3 position;
        #region Constructors
        public PlayerDead(Vector3 position)
        {
            this.position = position;
        }
        #endregion
    }
    public class PlayerRevived
    {
    }
    #endregion

    public class EnemyHurted
    {
        public GameObject enemy;
        public float damage;
        #region Constructors
        public EnemyHurted()
        {
        }

        public EnemyHurted(GameObject enemy, float damage)
        {
            this.enemy = enemy;
            this.damage = damage;
        }
        #endregion
    }
    public class EnemyDead
    {
        public GameObject enemy;
        public float souls;
        public EnemyDead(GameObject enemy, float souls)
        {
            this.enemy = enemy;
            this.souls = souls;
        }
    }
}