using UnityEngine;
using UnityEngine.UI;

public class UIChallenge : MonoBehaviour
{
    [SerializeField]
    private GameManager _manager;
    [SerializeField]
    private Text _currentScore;
    [SerializeField]
    private Text _maxScore;
    [SerializeField]
    private Text _timer;

    private void Update() {
        UpdateTimer();
        UpdateScore();
    }

    private void UpdateScore()
    {
        _currentScore.text = _manager.ValidatedCheckPoints.ToString();
        _maxScore.text = _manager.MaxCheckPoint.ToString();
    }

    private void UpdateTimer()
    {
        var time = _manager.TimerTime;
        var seconds = time % 60;
        var minutes = (time - seconds) / 60;
        _timer.text = $"{minutes :#00}:{seconds :#00.00}";
    }
}
