using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CarSuspension : MonoBehaviour
{
    private CarController _controller;
    private Transform _transform;
    private RaycastHit _currentHit;
    private float _currentSpringLength;
    private float _previousSpringLength;
    private bool _grounded;
    public bool Grounded => _grounded;

    [Header("Spring")]
    [SerializeField, Min(0), Tooltip("Length of the suspension")]
    private float _springLength;
    private float _springMaxExtend;
    private float _springMinExtend;
    [SerializeField, Min(0.5f), Tooltip("Amount of compression / extension the spring may accept")]
    private float _springCompressionCapacity;
    [SerializeField, Min(0)]
    private float _springStiffness;
    [SerializeField, Min(0)]
    private float _damperStiffness;

    [Header("Wheel")]
    [SerializeField, Min(0)]
    private float _wheelRadius;

    [Header("Gizmos")]
    [SerializeField]
    private bool _debug;
    [SerializeField]
    private bool _labels;
    [SerializeField]
    private Color _restfulColor = Color.green;
    [SerializeField]
    private Color _compressedColor = Color.red;
    private float _debugLength;

#if UNITY_EDITOR
    private void OnDrawGizmos() 
    {
        if(!_debug) return;

        var t = transform;
        RaycastHit hit;
        var compression = 1 - (_debugLength / _springLength);
        Color c = Color.Lerp(_restfulColor, _compressedColor, compression);
        Gizmos.color = c;
        Handles.color = c;
        if(Physics.Raycast(t.position, -t.up, out hit, _springLength + _wheelRadius))
        {
            var springEnd = hit.point + t.up * _wheelRadius;
            Gizmos.DrawLine(t.position, springEnd);
            Gizmos.DrawSphere(springEnd, 0.05f);
            Handles.DrawWireDisc(springEnd, t.right, _wheelRadius);
            _debugLength = hit.distance - _wheelRadius;
        }

        else
        {
            Gizmos.color = Color.red;
            Handles.color = Color.red;
            var springEnd = t.position - t.up * _springLength;
            Gizmos.DrawLine(t.position, springEnd);
            Gizmos.DrawSphere(springEnd, 0.05f);
            Handles.DrawWireDisc(springEnd, t.right, _wheelRadius);
            _debugLength = 0;
        }

        if(!_labels) return;
        
        Handles.color = Color.grey;
        Handles.Label(t.position, $"( {compression : #0.00} )");
    }
#endif

    private void Awake() 
    {
        _transform = transform;
        _controller = _transform.root.GetComponent<CarController>();
        _springMaxExtend = _springLength + _springCompressionCapacity;
        _springMinExtend = _springLength - _springCompressionCapacity;
    }

    private void FixedUpdate() 
    {
        UpdateSuspension();
    }

    private void UpdateSuspension()
    {   
        RaycastHit hit;
        if(Physics.Raycast(_transform.position, -_transform.up, out hit, _springMaxExtend + _wheelRadius))
        {
            _previousSpringLength = _currentSpringLength;
            _currentSpringLength = hit.distance - _wheelRadius;
            _currentSpringLength = Mathf.Clamp(_currentSpringLength, _springMinExtend, _springMaxExtend);
            _currentHit = hit;
            _grounded = true;
            ApplyCompensation();
        }

        else{
            _grounded = false;
        }
    }

    private void ApplyCompensation()
    {
        var springVelocity = (_previousSpringLength - _currentSpringLength) / Time.fixedDeltaTime;
        var springForceIntensity = _springStiffness * (_springLength - _currentSpringLength);
        var damperForceIntensity = _damperStiffness * springVelocity;
        var suspensionForce = (springForceIntensity + damperForceIntensity) * _transform.up;
        _controller.CarRigidbody.AddForceAtPosition(suspensionForce, _currentHit.point);
    }
}
