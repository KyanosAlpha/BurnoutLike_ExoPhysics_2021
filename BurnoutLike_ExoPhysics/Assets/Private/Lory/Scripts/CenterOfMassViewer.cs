using UnityEngine;

public class CenterOfMassViewer : MonoBehaviour
{
    #region fields
    [SerializeField]
    private bool _debug;
    [SerializeField]
    private Vector3 _centerOffset;
    private Rigidbody _carRigidbody;

    [SerializeField]
    private float _radius;
    #endregion

    #region unity messages
    private void OnDrawGizmos()
    {
        if(!_debug) return;
        
        var rb = GetComponent<Rigidbody>();
        var t = transform;
        Gizmos.color = Color.green;

        Gizmos.DrawSphere(t.position + rb.centerOfMass + _centerOffset, _radius);
    }

    private void Awake()
    {
        _carRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _carRigidbody.centerOfMass = _centerOffset;
    }
    #endregion unity messages
}