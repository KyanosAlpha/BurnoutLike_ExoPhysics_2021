using System;
using UnityEngine;
using UnityEngine.UI;

public class UIDefault : MonoBehaviour
{
    [SerializeField]
    private GameManager _manager;
    [SerializeField]
    private Text _bestTime;
    [SerializeField]
    private Text _lastAttemptTime;

    private void Awake() {
        _manager.OnDefaultState += UpdateTimes;
    }

    private void UpdateTimes()
    {
        _lastAttemptTime.text = FormatTime(_manager.LastAttemptTime);
        _bestTime.text = FormatTime(_manager.BestTime);
    }

    private string FormatTime(float time){
        if(time >= Mathf.Infinity || Mathf.Approximately(0, time)) return $"--:--";
        var seconds = time % 60;
        var minutes = (time - seconds) / 60;
        return $"{minutes :#00}:{seconds :#00.00}";
    }
}
