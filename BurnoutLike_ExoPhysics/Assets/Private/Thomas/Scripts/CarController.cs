using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    #region inspector
    [SerializeField, Range(0, 180)]
    private float _maxWheelRotation;  
    [SerializeField]
    private CarSuspension[] _drivingWheel;      
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
    }
    #endregion



    #region private methods
    private void UpdateSignedVelocity() => _signedVelocity = Vector3.Dot(_transform.forward, _rigidbody.velocity.normalized) >= 0 ? 1 : -1;

    private void CheckGround()
    {
        for(int i = 0; i < _drivingWheel.Length; i++){
            if(_drivingWheel[i].Grounded){
                _grounded = true;
                return;
            }

            else if(i == _drivingWheel.Length - 1){
                _grounded = false;
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