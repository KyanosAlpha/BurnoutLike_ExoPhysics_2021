using UnityEngine;
using System.Collections;

public enum GameState{
    Challenge,
    Chrono,
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
    [SerializeField]
    private Transform _spawnPoint;

    public int MaxCheckPoint => _checkpoints.Length;
    public int ValidatedCheckPoints => _validatedCheckpoints;
    public float ChronoTime => Mathf.Max(0, Time.time - _startTimerTime);
    public float ChallengeTime => Mathf.Max(0, _endChallengeTime - Time.time);
    public GameState GameState => _state;
    public float LastAttemptTime => _lastAttempt;
    public float BestTime => _bestTime;

    private Transform _player;
    private float _startTimerTime = 0;
    private float _endChallengeTime = 0;
    private IEnumerator _coroutine;
    private int _validatedCheckpoints = 0;
    private bool _challengeInput;
    private bool _abortInput;
    private bool _chronoInput;
    private float _lastAttempt;
    private float _bestTime  = Mathf.Infinity;

    public delegate void StateChange();
    public event StateChange OnDefaultState;
    public event StateChange OnChallengeState;

    private GameState _state;

    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _state = GameState.Default;
        _challengeUI.gameObject.SetActive(false);
        _defaultUI.gameObject.SetActive(true);
        _coroutine = ChallengeCountDown();

        foreach(var point in _checkpoints){
            point.CheckEvent += ValidateCheckpoint;
            point.gameObject.SetActive(false);
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
            LaunchChallenge(ChallengeCountDown(), GameState.Challenge);
        }

        else if(_state == GameState.Default && _chronoInput){
            LaunchChallenge(ActivateChrono(), GameState.Chrono);
        }
    }

    private void UpdateInputs()
    {
        _challengeInput = Input.GetKeyDown(KeyCode.E);
        _chronoInput = Input.GetKeyDown(KeyCode.R);
        _abortInput = Input.GetKeyDown(KeyCode.F);
    }

    private void LaunchChallenge(IEnumerator routine, GameState state){
        _state = state;
        SwitchUI();
        SetActiveCheckPoints(true);
        _player.position = _spawnPoint.position;
        _player.rotation = _spawnPoint.rotation;
        OnChallengeState?.Invoke();
        StartCoroutine(routine);
    }

    private void SetDefaultState(){
        _state = GameState.Default;
        SwitchUI();
        SetActiveCheckPoints(false);
        OnDefaultState?.Invoke();
        _validatedCheckpoints = 0;
    }

    private void StopChallenge(float time){
        _lastAttempt = time;
        if(_lastAttempt < _bestTime){
            _bestTime = _lastAttempt;
        }

        SetDefaultState();
        //validate the challenge
    }

    private void StopChrono(float time){
        _lastAttempt = time;
        if(_lastAttempt < _bestTime){
            _bestTime = _lastAttempt;
        }

        SetDefaultState();
    }

    private void Abort(){
        SetDefaultState();
        StopAllCoroutines();
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
        _startTimerTime = Time.time;
        _endChallengeTime = Time.time + _challengeTimer;
        while(!(_validatedCheckpoints == _checkpoints.Length)){
            yield return null;
            if(_abortInput || Time.time >= _endChallengeTime){
                Abort();
            }
        }

        StopChallenge(Time.time - _startTimerTime);
    }

    private IEnumerator ActivateChrono(){
        _startTimerTime = Time.time;
        while(!(_validatedCheckpoints == _checkpoints.Length)){
            yield return null;
            if(_abortInput){
                Abort();
            }
        }

        StopChrono(Time.time - _startTimerTime);
    }
}
