using UnityEngine;

public class VehiculeReverseGear : MonoBehaviour
{
    #region properties

    #endregion properties



    #region fields
    [SerializeField]
    private WheelCollider[] _wheelBack;
    [SerializeField]
    private Rigidbody _rigidbodyTutur;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _deceleration;
    private float _inputBrake;
    #endregion fields



    #region public methods

    #endregion public methods



    #region unity messages
    private void Awake()
    {
        
    }

    private void Start()
    {

    }

    private void Update()
    {
        GetInputBrake();
    }

    private void FixedUpdate()
    {
        VehiculeBrake();
    }
    #endregion unity messages



    #region private methods
    private void GetInputBrake()
    {
        _inputBrake = Input.GetAxis("Vertical") * _speed;
    }
    private void VehiculeBrake()
    {
        for (int i = 0; i < _wheelBack.Length; i++)
        {
            var wheelMotor = _wheelBack[i];

            wheelMotor.motorTorque = Mathf.Min(_inputBrake, 0);
        }
    }

    #endregion private methods
}