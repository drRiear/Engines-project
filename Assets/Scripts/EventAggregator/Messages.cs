public class Messages
{
    public class Interaction
    {
        public int group;
    }
    public class Cross
    {
        public int group;
    }
    public class PlayerHurted
    {
        public int group;
        public float damage;

        #region Constructors
        public PlayerHurted()
        {
        }

        public PlayerHurted(float damage)
        {
            this.damage = damage;
        }

        public PlayerHurted(int group, float damage)
        {
            this.group = group;
            this.damage = damage;
        }
        #endregion
    }
    public class PlayerJump
    {
        public int group;
    }
    public class PlayerDroped
    {
        public int group;
    }
}