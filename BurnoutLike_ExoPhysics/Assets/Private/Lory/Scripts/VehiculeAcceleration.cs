using UnityEngine;

public class VehiculeAcceleration : MonoBehaviour
{
    #region properties

    #endregion properties



    #region fields
    [SerializeField]
    private WheelCollider[] _wheelBack;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _deceleration;
    private float _inputAccelerator;
    #endregion fields



    #region public methods

    #endregion public methods



    #region unity messages
    private void OnGUI()
    {
        GUILayout.Button($"InputAccelerator: {_inputAccelerator: 00.00}");
    }

    private void Awake()
    {
        
    }

    private void Start()
    {

    }

    private void Update()
    {
        GetInputAcceleration();
    }

    private void FixedUpdate()
    {
        VehiculeAccelerator();
    }
    #endregion unity messages



    #region private methods
    private void GetInputAcceleration()
    {
        _inputAccelerator = Input.GetAxis("Vertical") * _speed;
    }

    private void VehiculeAccelerator()
    {
        for (int i = 0; i < _wheelBack.Length; i++)
        {
            var wheelMotor = _wheelBack[i];

            wheelMotor.motorTorque = Mathf.Max(_inputAccelerator, 0);

            if (_inputAccelerator <= 0 && wheelMotor.motorTorque > 0)
            {
                wheelMotor.motorTorque = Mathf.Max(0f, wheelMotor.motorTorque - _deceleration);
            }
        }
    }
    #endregion private methods
}