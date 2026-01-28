using UnityEngine;

[System.Serializable]
public class RewardData
{
    [SerializeField] private int scoreValue;
    [SerializeField] private int gemScore;

    public int ScoreValue
    {
        get { return scoreValue; }
    }
    public int GemScore
    {
        get { return gemScore; }
    }

    public RewardData(int scoreValue, int gemScore)
    {
        this.scoreValue = scoreValue;
        this.gemScore = gemScore;
    }
}
