using UnityEngine;

public class Test_WheelCollider : MonoBehaviour
{
    #region Public Members

    public WheelCollider wheelCollider;

    #endregion


    #region Unity API

    private void Awake()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }

    private void Update()
    {
        var axeVertical = Input.GetAxis("Vertical");

        Debug.Log(axeVertical);

        wheelCollider.motorTorque = axeVertical * 200;
    }

    #endregion


    #region Private And Protected

    #endregion
}