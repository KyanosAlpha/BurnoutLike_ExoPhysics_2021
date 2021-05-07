using UnityEngine;
using System.Collections;

public enum GameState{
    InChallenge,
    Default
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Checkpoint[] _checkpoints;
    [SerializeField]
    private RectTransform _challengeUI;
    [SerializeField]
    private RectTransform _defaultUI;
    [SerializeField]
    private float _challengeTimer;

    public int MaxCheckPoint => _checkpoints.Length;
    public int ValidatedCheckPoints => _validatedCheckpoints;
    public float TimerTime => Mathf.Max(0, _endTime - Time.time);

    private float _endTime = 0;
    private IEnumerator _coroutine;
    private int _validatedCheckpoints = 0;
    private bool _challengeInput;
    private bool _abortInput;

    public delegate void StateChange();
    public event StateChange OnDefaultState;
    public event StateChange OnChallengeState;

    private GameState _state;

    private void Awake() {
        _state = GameState.Default;
        _challengeUI.gameObject.SetActive(false);
        _defaultUI.gameObject.SetActive(true);
        _coroutine = ChallengeCountDown();

        foreach(var point in _checkpoints){
            point.CheckEvent += ValidateCheckpoint;
        }
    }

    private void ValidateCheckpoint() => _validatedCheckpoints ++;

    private void Update() {
        UpdateInputs();
        CheckForStateChange();
    }

    private void CheckForStateChange()
    {
        if(_state == GameState.Default && _challengeInput){
            LaunchChallenge();
        }

        else if(_state == GameState.InChallenge && _abortInput){
            AbortChallenge();
        }
    }

    private void UpdateInputs()
    {
        _challengeInput = Input.GetKeyDown(KeyCode.E);
        _abortInput = Input.GetKeyDown(KeyCode.F);
    }

    private void LaunchChallenge(){
        _state = GameState.InChallenge;
        SwitchUI();
        SetActiveCheckPoints(true);
        OnChallengeState.Invoke();
        StartCoroutine(_coroutine);
    }

    private void StopChallenge(){
        _state = GameState.Default;
        SwitchUI();
        SetActiveCheckPoints(false);
        OnDefaultState.Invoke();
        _validatedCheckpoints = 0;
    }

    private void AbortChallenge(){
        StopCoroutine(_coroutine);
        StopChallenge();
    }

    private void SwitchUI(){
        _challengeUI.gameObject.SetActive(!_challengeUI.gameObject.activeInHierarchy);
        _defaultUI.gameObject.SetActive(!_defaultUI.gameObject.activeInHierarchy);
    }

    private void SetActiveCheckPoints(bool activate){
        for(int i = 0; i < _checkpoints.Length; i++){
            _checkpoints[i].gameObject.SetActive(activate);
        }
    }

    private IEnumerator ChallengeCountDown(){
        _endTime = Time.time + _challengeTimer;
        while(Time.time < _endTime){
            yield return null;
            if(_validatedCheckpoints == _checkpoints.Length){
                AbortChallenge();
            }
        }

        StopChallenge();
    }
}
