using UnityEngine;

public class CarAcceleration : MonoBehaviour
{
    #region fields
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _brakeIntensity;
    [SerializeField]
    private float _rotationSpeed;
    #endregion fields



    #region unity messages
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CarAccelerationAndReverseGear();
        CarRotation();
        Carbrake();
    }
    #endregion unity messages



    #region private methods
    private void CarAccelerationAndReverseGear()
    {
        var intensity = Input.GetAxis("Vertical") * _acceleration;
        _rigidbody.velocity += intensity * transform.forward;
    }

    private void CarRotation()
    {
        _rigidbody.AddTorque(transform.up * (Input.GetAxis("Horizontal") * _rotationSpeed), ForceMode.Force);
    }

    private void Carbrake()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            var velocity = _rigidbody.velocity;
            var brake = velocity.normalized / _brakeIntensity;

            var deltaX = velocity.x - brake.x;
            var deltaY = velocity.y - brake.y;
            var deltaZ = velocity.z - brake.z;

            if(deltaX * velocity.x <= 0)
            {
                deltaX = 0;
            }
            if(deltaY * velocity.y <= 0)
            {
                deltaY = 0;
            }
            if (deltaZ * velocity.z <= 0)
            {
                deltaZ = 0;
            }

            Vector3 newVelocity = new Vector3(deltaX, deltaY, deltaZ);

            _rigidbody.velocity = newVelocity;
        }
    }
    #endregion private methods
}