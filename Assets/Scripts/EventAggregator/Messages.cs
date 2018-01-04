using UnityEngine;

public class Messages
{
    public class Interaction
    {
    }
    public class Cross
    {
    }
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
}