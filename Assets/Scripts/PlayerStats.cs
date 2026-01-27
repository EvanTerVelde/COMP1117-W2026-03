using UnityEngine;

[System.Serializable]
public class PlayerStats : StatsData
{
    [SerializeField] private int lives;
    
    public int Lives
    {
        get { return lives; }
    }

    public PlayerStats(int hp, int lives) : base(hp)
    {
        this.lives = lives;
    }

    public void LoseLife()
    {
        if(lives > 0)
        {
            lives--;
        }
    }
}
