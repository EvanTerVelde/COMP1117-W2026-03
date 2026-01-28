using UnityEngine;

[System.Serializable]
public class EnemyStats : StatsData
{
    [SerializeField] private RewardData rewards;     // Composition!

    public RewardData Rewards
    {
        get { return rewards; }
    }

    public EnemyStats(int hp, int score, int gems) : base(hp)
    {
        rewards = new RewardData(score, gems);
    }
}
