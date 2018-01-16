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
        public float coins;
        #region Constructors
        public CoinPicketUp()
        {
        }
        public CoinPicketUp(float coins)
        {
            this.coins = coins;
        }
        #endregion
    }
    public class SoulsPicketUp
    {
        public float souls;
        #region Constructors
        public SoulsPicketUp()
        {
        }
        public SoulsPicketUp(float souls)
        {
            this.souls = souls;
        }
        #endregion
    }

    #region Player Messages
    public class PlayerHurted
    {
        public float damage;
        #region Constructors
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

    public class PlayerLevelUp
    {
        public PlayerStats.MainStats stat;

        public PlayerLevelUp(PlayerStats.MainStats stat)
        {
            this.stat = stat;
        }
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
        public EnemyDead(GameObject enemy)
        {
            this.enemy = enemy;
        }
    }

    public class StatsWindowOpened
    {

    }
}