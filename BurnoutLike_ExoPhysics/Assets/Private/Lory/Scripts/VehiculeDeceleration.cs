using UnityEngine;

public class VehiculeDeceleration : MonoBehaviour
{
    #region properties

    #endregion properties



    #region fields
    [SerializeField]
    private WheelCollider[] _wheelBack;
    private Rigidbody _rigidbodyTutur;

    private float _inputBrake;
    #endregion fields



    #region public methods

    #endregion public methods



    #region unity messages
    private void OnGUI()
    {
        //GUILayout.Button($"Velocity: {_rigidbodyTutur.velocity.magnitude: 00.00}");
        //GUILayout.Button($"Motor: {_wheelBack[0].motorTorque: 00.00}");
        //GUILayout.Button($"Brake: {_wheelBack[0].brakeTorque: 00.00}");
        //GUILayout.Button($"InputBrake: {_inputBrake: 00.00}");
    }

    private void Awake()
    {
        InitializeOnAwake();
    }

    private void Start()
    {

    }

    private void Update()
    {
        _inputBrake = Mathf.Min(0f, Input.GetAxis("Vertical"));
        _inputBrake = Mathf.Abs(_inputBrake);
    }

    private void FixedUpdate()
    {
        VehiculeBrake();
    }
    #endregion unity messages



    #region private methods
    private void InitializeOnAwake()
    {
        _rigidbodyTutur = GetComponent<Rigidbody>();
    }

    private void VehiculeBrake()
    {
        for(int i = 0; i < _wheelBack.Length; i++)
        {
            if ( _inputBrake >= 0)
            {
                _wheelBack[i].brakeTorque = _inputBrake * 1000f;
            }
        }
    }

    #endregion private methods
}