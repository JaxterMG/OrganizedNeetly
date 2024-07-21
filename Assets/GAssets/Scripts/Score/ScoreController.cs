using Core.EventBus;
using Core.Save;
using Core.UI.Score;
using Newtonsoft.Json;
using UnityEngine;
using Core.EventBus;

namespace Core.Score
{
    public class ScoreController : IScoreController, ISavable
    {
        private EventBus.EventBus _eventBus;
        private ScoreView _scoreView;

        public int Points { get; private set; }

        private int _highScore;

        public ScoreController(EventBus.EventBus eventBus, ScoreView scoreView)
        {
            _eventBus = eventBus;
            _scoreView = scoreView;

            GameObject.FindObjectOfType<SaveLoadHandler>().RegisterSavable(this);
            _eventBus.Subscribe<int>(BusEventType.IncreaseScore, AddPoints);
            _highScore = PlayerPrefs.GetInt("HighScore", 0);
            _scoreView.UpdateHighScore(_highScore);
        }

        public void AddPoints(int points)
        {
            Points += points;
            if (Points > _highScore)
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

        public int GetHighScore()
        {
            return _highScore;
        }

        private struct ScoreData
        {
            public int Score;
            public int HighScore;

            public ScoreData(int score, int highScore)
            {
                Score = score;
                HighScore = highScore;
            }
        }

        public void Load(string jsonData)
        {
            var data = JsonConvert.DeserializeObject<ScoreData>(jsonData);
            Points = data.Score;
            _scoreView.UpdateHighScore(_highScore);
            _scoreView.UpdateScore(Points);
        }

        public string Save()
        {
            return JsonConvert.SerializeObject(new ScoreData(Points, _highScore));
        }
    }

    public interface IScoreController
    {
        public void AddPoints(int points);
        public int GetPoints();
        public int GetHighScore();
        public bool IsHighScore();
    }
}
