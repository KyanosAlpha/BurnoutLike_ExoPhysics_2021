using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private GameManager _manager;
    [SerializeField]
    private AudioSource _challengeSource;
    [SerializeField]
    private AudioSource _defaultSource;

    private void Awake() {


        _manager.OnChallengeState += EnterChallenge;
        _manager.OnDefaultState += ExitChallenge;
    }

    private void ExitChallenge()
    {
        _challengeSource.Stop();
        _defaultSource.Play();
    }

    private void EnterChallenge()
    {
        _challengeSource.Play();
        _defaultSource.Stop();
    }
}
