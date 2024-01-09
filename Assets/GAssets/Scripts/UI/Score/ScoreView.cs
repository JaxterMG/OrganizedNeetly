using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    public TextMeshProUGUI CurrentScore;
    public TextMeshProUGUI HighScore;

    public void UpdateScore(int score)
    {
        CurrentScore.text = score.ToString();
    }
    public void UpdateHighScore(int score)
    {
        HighScore.text = score.ToString();
    }

}