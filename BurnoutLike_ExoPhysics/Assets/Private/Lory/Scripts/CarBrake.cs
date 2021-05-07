using UnityEngine;

public class CarBrake : MonoBehaviour
{
    #region properties
    #endregion properties



    #region fields
    private Rigidbody _carRigidbody;
    private Transform _carTransform;

    [SerializeField]
    private float _carBrakeIntensity;
    #endregion fields



    #region public methods
    #endregion public methods



    #region unity messages
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
        Carbrake();
    }
    #endregion unity messages



    #region private methods
    private void Carbrake()
    {
        if (Input.GetKey(KeyCode.LeftShift))
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

            if (deltaX * velocityX <= 0)
            {
                deltaX = 0;
            }
            if (deltaY * velocityY <= 0)
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
    #endregion private methods
}