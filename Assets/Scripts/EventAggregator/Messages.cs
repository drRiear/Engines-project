using Player;
using UnityEngine;

namespace Messages
{
    public class Dialogue
    {
        public class Start
        {
            public GameObject npc;

            public Start(GameObject npc)
            {
                this.npc = npc;
            }
        }

        public class Stops
        {
            public GameObject npc;

            public Stops(GameObject npc)
            {
                this.npc = npc;
            }
        }
        public class GetAnswer
        {
            public DialogueSystem.Lines.Answer answer;

            public GetAnswer(DialogueSystem.Lines.Answer answer)
            {
                this.answer = answer;
            }
        }
    }

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

    public class Player
    {
        public class Hurted
        {
            public float damage;

            #region Constructors

            public Hurted(float damage)
            {
                this.damage = damage;
            }

            #endregion
        }

        public class Jump
        {
        }

        public class Landed
        {
        }

        public class Dead
        {
            public Vector3 position;

            #region Constructors

            public Dead(Vector3 position)
            {
                this.position = position;
            }

            #endregion
        }

        public class Revived
        {
        }

        public class LevelUp
        {
            public Stats.MainStats stat;

            public LevelUp(Stats.MainStats stat)
            {
                this.stat = stat;
            }
        }
    }

    public class Enemy
    {
        public class Hurted
        {
            public GameObject enemy;
            public float damage;

            #region Constructors

            public Hurted()
            {
            }

            public Hurted(GameObject enemy, float damage)
            {
                this.enemy = enemy;
                this.damage = damage;
            }

            #endregion
        }

        public class Dead
        {
            public GameObject enemy;

            public Dead(GameObject enemy)
            {
                this.enemy = enemy;
            }
        }
    }

    public class StatsWindowOpened
    {

    }
}