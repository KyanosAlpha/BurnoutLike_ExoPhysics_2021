using UnityEngine;

public class Suspension : MonoBehaviour
{
    private void Awake() {
        _transform = transform;
    }

    private void Update() {
        var position = Vector3.zero;
        var rotation = Quaternion.identity;
        _collider.GetWorldPose(out position, out rotation);

        _transform.position = position;
        _transform.rotation = rotation;
    }

    [SerializeField]
    private WheelCollider _collider;
    private Transform _transform;
}
