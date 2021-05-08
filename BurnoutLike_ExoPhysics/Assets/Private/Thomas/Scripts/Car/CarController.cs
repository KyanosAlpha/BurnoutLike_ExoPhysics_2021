using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    #region inspector
    [SerializeField, Range(0, 180)]
    private float _maxWheelRotation;  
    [SerializeField, Min(0)]
    private float _minVelocityTreshold;
    [SerializeField, Min(0)]
    private float _groundedAngularDrag;
    [SerializeField, Min(0)]
    private float _inAirAngularDrag;
    [SerializeField]
    private CarSuspension[] _drivingWheels;      
    [SerializeField]
    private CarSuspension[] _turningWheels;      
    #endregion



    #region public properties
    public Vector3 WheelOrientation
    {
        get => _currentWheelEulerianRotation;
        set => _currentWheelEulerianRotation = value;
    }

    public Rigidbody CarRigidbody => _rigidbody;
    public Transform CarTransform => _transform;
    public bool Grounded => _grounded;
    public float MaxWheelRotation => _maxWheelRotation;
    public float SignedVelocity => _signedVelocity;
    #endregion



    #region unity api
    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void Update() 
    {
        CheckGround();
        UpdateSignedVelocity();
        ClampVelocity();
        ApplyWheelAngleToSuspensions();
    }

    private void ApplyWheelAngleToSuspensions()
    {
        for (int i = 0; i < _turningWheels.Length; i++)
        {
            var suspension =  _turningWheels[i];
            suspension.transform.rotation = Quaternion.Euler(_currentWheelEulerianRotation);
        }
    }

    private void ClampVelocity()
    {
        var velocity = _rigidbody.velocity;
        if(velocity.magnitude < _minVelocityTreshold){
            _rigidbody.velocity = new Vector3(0, velocity.y, 0);
        }
    }
    #endregion



    #region private methods
    private void UpdateSignedVelocity() => _signedVelocity = Vector3.Dot(_transform.forward, _rigidbody.velocity.normalized) >= 0 ? 1 : -1;

    private void CheckGround()
    {
        for(int i = 0; i < _drivingWheels.Length; i++){
            if(_drivingWheels[i].Grounded){
                _grounded = true;
                _rigidbody.angularDrag = _groundedAngularDrag;
                return;
            }

            else if(i == _drivingWheels.Length - 1){
                _grounded = false;
                _rigidbody.angularDrag = _inAirAngularDrag;
            }
        }
    }
    #endregion
        


    #region private fields
    private Vector3 _currentVelocity;
    private Vector3 _currentWheelEulerianRotation;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private bool _grounded;   
    private float _signedVelocity;   
    #endregion
}