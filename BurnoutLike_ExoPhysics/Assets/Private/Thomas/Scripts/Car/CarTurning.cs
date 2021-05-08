using UnityEngine;

public class CarTurning : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _turningCurve;
    private CarController _controller;
    private float _input;
    [SerializeField]
    private float _wheelSmoothness;
    [SerializeField]
    private float _torqueMultiplier;
    private float _currentRotation;
    private float _interpolationVelocity;

    private void Awake() 
    {
        _controller = transform.root.GetComponent<CarController>();
        if(_controller == null)
        {
            Debug.LogError("No CarController found on the root of this gameObject", this);
        }
    }

    private void Update() 
    {
        UpdateInput();
        InterpolateWheelOrientation();
    }

    private void FixedUpdate() {
        RotateCar();
    }

    private void RotateCar()
    {
        if(!_controller.Grounded) return;

        var wheelRotation = _controller.WheelOrientation.y * Time.fixedDeltaTime;
        var velocity =_controller.CarRigidbody.velocity;
        var velocity2D = new Vector3(velocity.x, 0, velocity.z);
        var turnCapacity = _turningCurve.Evaluate(velocity2D.magnitude);
        _controller.CarRigidbody.angularVelocity = (_torqueMultiplier * Vector3.up * wheelRotation * turnCapacity);
    }

    private void InterpolateWheelOrientation()
    {
        var aimedRotation = _input * _controller.MaxWheelRotation * _controller.SignedVelocity;
        var wheelRotation = _controller.WheelOrientation;
        wheelRotation.y = Mathf.SmoothDamp(wheelRotation.y, aimedRotation, ref _interpolationVelocity, _wheelSmoothness);
        _controller.WheelOrientation = wheelRotation;
    }

    private void UpdateInput() => _input = Input.GetAxis("Horizontal");
}
