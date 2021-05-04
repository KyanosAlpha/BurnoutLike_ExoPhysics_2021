using UnityEngine;

public class VehiculeSteer : MonoBehaviour
{
    #region properties

    #endregion properties



    #region fields
    [SerializeField]
    private WheelCollider[] _wheelFront;

    [SerializeField]
    private float _degreeSteerMax;
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

    }

    private void FixedUpdate()
    {
        for(int i = 0; i < _wheelFront.Length; i++)
        {
            _wheelFront[i].steerAngle = Input.GetAxis("Horizontal") * _degreeSteerMax;
        }
    }
    #endregion unity messages



    #region private methods

    #endregion private methods
}