using UnityEngine;

public class CarGrip : MonoBehaviour
{
    private CarController _controller;
    [SerializeField]
    private float _maxCompensation;
    [SerializeField]
    private bool _debug;

    private void Awake() {
        _controller = transform.root.GetComponent<CarController>();
        if(_controller == null)
        {
            Debug.LogError("No CarController found on the root of this gameObject", this);
        }
    }

    private void FixedUpdate() {
        SidewayCompensation();
    }

    private void SidewayCompensation()
    {
        if(!_controller.Grounded) return;

        var velocity = _controller.CarRigidbody.velocity;
        var angleFromVelocity = Vector3.Angle(_controller.CarTransform.right, _controller.CarRigidbody.velocity);
        var sidewayIntensity = Mathf.Cos(angleFromVelocity * Mathf.Deg2Rad) * velocity.magnitude;
        sidewayIntensity = Mathf.Clamp(sidewayIntensity, -_maxCompensation, _maxCompensation);
        var compensativeForce = _controller.CarTransform.right * -sidewayIntensity;

        var newVelocity = _controller.CarRigidbody.velocity;
        newVelocity += compensativeForce;
        _controller.CarRigidbody.velocity = newVelocity;
    }

    private void OnDrawGizmos() {
        if(_controller == null || !_debug) return;

        var t = transform;
        var v = _controller.CarRigidbody.velocity;
        var a = Vector3.Angle(_controller.CarTransform.right, _controller.CarRigidbody.velocity);
        var cLength = Mathf.Cos(a * Mathf.Deg2Rad) * v.magnitude;

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(t.position, t.position + _controller.CarTransform.right * cLength);
    }
}