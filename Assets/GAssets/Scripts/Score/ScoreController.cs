using Newtonsoft.Json;
using UnityEngine;

public class ScoreController : IScoreController, ISavable
{
    private EventBus _eventBus;
    private ScoreView _scoreView;
    
    public int Points{get; private set;}

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
        Points += points;
        if(Points > _highScore)
        {
            PlayerPrefs.SetInt("HighScore", Points);
            _scoreView.UpdateHighScore(Points);
        }
        _scoreView.UpdateScore(Points);
    }

    public int GetPoints()
    {
        return Points;
    }

    public bool IsHighScore()
    {
        return Points < _highScore ? false : true;
    }

    public void Load(string jsonData)
    {
        var data = JsonConvert.DeserializeObject<ScoreController>(jsonData);
        Points = data.Points;
    }

    public string Save()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public interface IScoreController
{
    public void AddPoints(int points);
    public int GetPoints();
    public bool IsHighScore();
}
