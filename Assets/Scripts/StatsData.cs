using UnityEngine;

[System.Serializable] // Tells Unity to draw this class in the inspector
public class StatsData
{
    [SerializeField] private int maxHP;

    public int MaxHP
    {
        get { return maxHP; }
    }

    public StatsData(int maxHP)
    {
        this.maxHP = maxHP;
    }
}
