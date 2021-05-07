using UnityEngine;

public class CarTurning : MonoBehaviour
{
    private CarController _controller;
    private float _input;
    [SerializeField]
    private float _wheelSmoothness;
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
        var wheelRotation = _controller.WheelOrientation.y * Time.fixedDeltaTime;
        _controller.CarRigidbody.rotation *= Quaternion.Euler(Vector3.up * wheelRotation);
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
