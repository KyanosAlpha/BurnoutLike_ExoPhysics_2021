using UnityEngine;

public class VehiculeMovement : MonoBehaviour
{
    #region properties

    #endregion properties



    #region fields
    [SerializeField]
    private WheelCollider[] _wheelBack;
    #endregion fields



    #region public methods

    #endregion public methods



    #region unity messages
    private void Awake()
    {
        InitialisationOnAwake();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        VehiculeAccelerator();
    }
    #endregion unity messages



    #region private methods
    private void InitialisationOnAwake()
    {
        _wheelBack = GetComponentsInChildren<WheelCollider>();
    }

    private void VehiculeAccelerator()
    {
        var accelerator = Input.GetAxis("Vertical") * 100f;
        for (int i = 0; i < _wheelBack.Length; i++)
        {
            _wheelBack[i].motorTorque += accelerator;
        }
    }
    #endregion private methods
}