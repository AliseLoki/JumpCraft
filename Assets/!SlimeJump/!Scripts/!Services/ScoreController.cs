using System;

public class ScoreController 
{
    private int _score;

    public event Action<int> ScoreChanged;

    public void OnScoreChanged(int bonusScore)
    {
        _score += bonusScore;
        ScoreChanged?.Invoke(_score);
    }
}
