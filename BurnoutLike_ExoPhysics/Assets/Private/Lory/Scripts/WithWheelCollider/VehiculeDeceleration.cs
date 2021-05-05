using UnityEngine;

public class VehiculeDeceleration : MonoBehaviour
{
    #region properties

    #endregion properties



    #region fields
    [SerializeField]
    private WheelCollider[] _wheelBack;
    private Rigidbody _rigidbodyTutur;
    #endregion fields



    #region public methods

    #endregion public methods



    #region unity messages
    private void OnGUI()
    {
        GUILayout.Button($"Velocity: {_rigidbodyTutur.velocity.magnitude: 00.00}");
        GUILayout.Button($"Motor: {_wheelBack[0].motorTorque: 00.00}");
        GUILayout.Button($"Brake: {_wheelBack[0].brakeTorque: 00.00}");
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
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _wheelBack[i].brakeTorque = 1500f;
            }
            else
            {
                _wheelBack[i].brakeTorque = 0f;
            }
        }
    }

    #endregion private methods
}