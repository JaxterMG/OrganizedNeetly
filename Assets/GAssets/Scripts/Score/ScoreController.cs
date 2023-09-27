using Zenject;

public class ScoreController : IScoreController
{
    [Inject]
    private ScoreView _scoreView;

    private int _points;
    public void AddPoints(int points)
    {
        _points += points;

        _scoreView.UpdateScore(_points);
    }

    public int GetPoints()
    {
        return _points;
    }
}

public interface IScoreController
{
    public void AddPoints(int points);
    public int GetPoints();
}
