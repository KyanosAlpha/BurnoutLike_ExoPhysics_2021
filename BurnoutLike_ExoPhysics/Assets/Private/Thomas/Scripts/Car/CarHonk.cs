using System;
using UnityEngine;

public enum HonkState{
    Begin,
    Hold,
    None
}

public class CarHonk : MonoBehaviour
{
    private AudioSource _source;
    [SerializeField]
    private AudioClip _honkBegin;
    [SerializeField]
    private AudioClip _honkHold;
    private bool _input;
    private HonkState _state;
    private float _endHonkTime;

    private void Awake() {
        _source = GetComponent<AudioSource>();
        _state = HonkState.None;
    }

    private void Update() {
        UpdateInput();
        CheckBeginHonk();
        CheckHoldHonk();
        CheckStopHonk();
    }

    private void CheckBeginHonk()
    {
        if(!(_input && _state == HonkState.None)) return;

        _source.clip = _honkBegin;
        _source.Play();
        _endHonkTime = Time.time + _source.clip.length;
        _state = HonkState.Begin;
    }

    private void CheckHoldHonk()
    {
        if(!(_input && CheckEndOfBegin() && _state == HonkState.Begin)) return;

        _source.clip = _honkHold;
        _source.loop = true;
        _source.Play();
        _state = HonkState.Hold;
    }

    private void CheckStopHonk()
    {
        if(_input) return;
        
        _source.Stop();
        _source.loop = false;
        _state = HonkState.None;
    }

    private bool CheckEndOfBegin()
    {
        return Time.time >= _endHonkTime;
    }

    private void UpdateInput(){
        if(Input.GetKeyDown(KeyCode.Space)){
            _input = true;
        }

        else if(Input.GetKeyUp(KeyCode.Space)){
            _input = false;
        }
    }
}
