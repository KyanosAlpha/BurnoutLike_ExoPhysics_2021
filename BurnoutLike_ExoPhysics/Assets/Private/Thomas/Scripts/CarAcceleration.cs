using UnityEngine;

public class CarAcceleration : MonoBehaviour
{
    private CarController _controller;
    private float _input;
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private bool _debugApplyedAccelerationOrientation;

    private void Awake() {
        _controller = transform.root.GetComponent<CarController>();
        if(_controller == null)
        {
            Debug.LogError("No CarController found on the root of this gameObject", this);
        }
    }

    private void Update() {
        UpdateInput();
    }

    private void FixedUpdate() {
        Accelerate();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() 
    {
        if(_controller == null || !_debugApplyedAccelerationOrientation) return;

        var wheelForward = Quaternion.Euler(Vector3.up * _controller.WheelOrientation.y) * _controller.CarTransform.forward;
        var velocityToAdd = wheelForward * _input * _acceleration * Time.fixedDeltaTime;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + velocityToAdd);
    }
#endif

    private void Accelerate()
    {
        if(!_controller.Grounded) return;

        var wheelForward = Quaternion.Euler(Vector3.up * _controller.WheelOrientation.y) * _controller.CarTransform.forward;
        var velocityToAdd = wheelForward * _input * _acceleration * Time.fixedDeltaTime;
        _controller.CarRigidbody.velocity += velocityToAdd;
    }

    private void UpdateInput() => _input = Input.GetAxis("Vertical");
}
