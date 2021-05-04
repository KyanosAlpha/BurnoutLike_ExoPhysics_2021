using UnityEngine;

public class Test_WheelCollider : MonoBehaviour
{
    #region Unity API

    private void Awake()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }

    private void Update() {
        axeVertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        wheelCollider.motorTorque = axeVertical * torque;
    }

    #endregion


    #region Private Properties
    
    [SerializeField]
    public float torque = 200;
    private WheelCollider wheelCollider;
    private float axeVertical;

    #endregion
}