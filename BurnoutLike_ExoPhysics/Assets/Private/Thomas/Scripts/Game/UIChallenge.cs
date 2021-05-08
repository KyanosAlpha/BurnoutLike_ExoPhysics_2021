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

    [Header("Challenge")]
    [SerializeField]
    private float _criticalTime;
    [SerializeField]
    private Color _challengeCriticalColor;

    [Header("Chrono")]
    [SerializeField]
    private Color _chronoColor;

    private float _time;


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
        _time = _manager.GameState == GameState.Chrono ? _manager.ChronoTime : _manager.ChallengeTime;
        var seconds = _time % 60;
        var minutes = (_time - seconds) / 60;
        _timer.text = $"{minutes :#00}:{seconds :#00.00}";

        _timer.color = SetTimeColor();
    }

    private Color SetTimeColor(){
        var color = Color.white;
        if(_manager.GameState == GameState.Challenge && _time <= _criticalTime){
            color = _challengeCriticalColor;
        }

        else if(_manager.GameState == GameState.Chrono){
            color = _chronoColor;
        }

        return color;
    }
}
