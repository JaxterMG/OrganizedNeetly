using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ScoreController : IScoreController
{
    private EventBus _eventBus;
    private ScoreView _scoreView;
    
    private int _points;

    private int _highScore;
    
    public ScoreController(EventBus eventBus, ScoreView scoreView)
    {
        _eventBus = eventBus;
        _scoreView = scoreView;

        _eventBus.Subscribe<int>(EventType.IncreaseScore, AddPoints);
        PlayerPrefs.DeleteKey("HighScore");
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        _scoreView.UpdateHighScore(_highScore);
    }
    public void AddPoints(int points)
    {
        _points += points;
        if(_points > _highScore)
        {
            PlayerPrefs.SetInt("HighScore", _points);
            _scoreView.UpdateHighScore(_points);
        }
        _scoreView.UpdateScore(_points);
    }

    public int GetPoints()
    {
        return _points;
    }

    public bool IsHighScore()
    {
        return _points < _highScore ? false : true;
    }
}

public interface IScoreController
{
    public void AddPoints(int points);
    public int GetPoints();
    public bool IsHighScore();
}
