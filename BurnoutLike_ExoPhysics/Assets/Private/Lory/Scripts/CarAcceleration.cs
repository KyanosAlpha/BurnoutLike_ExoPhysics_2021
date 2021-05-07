using UnityEngine;

public class CarAcceleration : MonoBehaviour
{
    #region properties
    #endregion properties



    #region fields
    private Rigidbody _carRigidbody;
    private Transform _carTransform;

    [SerializeField]
    private float _carSpeed;
    [SerializeField]
    private float _carBrakeIntensity;
    [SerializeField]
    private float _carRotationSpeed;
    private float _carRotationFactor;
    [SerializeField]
    private float _magnitude;
    #endregion fields



    #region public methods
    #endregion public methods



    #region unity messages
    private void OnGUI()
    {
        var rotation = _carRigidbody.rotation.eulerAngles;

        GUILayout.Button($"Value Rotation: {rotation}");
    }

    private void Awake()
    {
        _carRigidbody = GetComponent<Rigidbody>();
        _carTransform = transform;
    }

    private void Start()
    {

    }

    private void Update()
    {
        
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
        if (Input.GetAxis("Vertical") > 0)
        {
            _carRigidbody.velocity += _carTransform.forward * _carSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            _carRigidbody.velocity -= _carTransform.forward * _carSpeed * Time.fixedDeltaTime;
        }
    }

    private void CarRotation()
    {
        if (_carRigidbody.velocity.magnitude < _magnitude) return;
        SignedVelocity();
        var rigidbodyRotation = _carRigidbody.rotation.eulerAngles;
        var newRotation = _carTransform.up * Input.GetAxis("Horizontal") * _carRotationSpeed * _carRotationFactor * Time.fixedDeltaTime;

        rigidbodyRotation += newRotation;
        _carRigidbody.rotation = Quaternion.Euler(rigidbodyRotation);
    }

    private void Carbrake()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            var velocity = _carRigidbody.velocity;
            var brake = velocity.normalized / _carBrakeIntensity * Time.fixedDeltaTime;

            var velocityX = velocity.x;
            var velocityY = velocity.y;
            var velocityZ = velocity.z;

            var brakeX = brake.x;
            var brakeY = brake.y;
            var brakeZ = brake.z;

            var deltaX = velocityX - brakeX;
            var deltaY = velocityY - brakeY;
            var deltaZ = velocityZ - brakeZ;

            if(deltaX * velocityX <= 0)
            {
                deltaX = 0;
            }
            if(deltaY * velocityY <= 0)
            {
                deltaY = 0;
            }
            if (deltaZ * velocityZ <= 0)
            {
                deltaZ = 0;
            }

            Vector3 newVelocity = new Vector3(deltaX, deltaY, deltaZ);

            _carRigidbody.velocity = newVelocity;
        }
    }

    private void SignedVelocity()
    {
        var rigidbodyVelocity = _carRigidbody.velocity;
        var carForward = _carTransform.forward;
        var dotProduct = Vector3.Dot(carForward, rigidbodyVelocity);

        _carRotationFactor = dotProduct >= 0.001f ? 1 : -1;
    }
    #endregion private methods
}