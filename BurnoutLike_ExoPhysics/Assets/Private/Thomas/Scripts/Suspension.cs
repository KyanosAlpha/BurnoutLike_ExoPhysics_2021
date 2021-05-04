using UnityEngine;

public class Suspension : MonoBehaviour
{
    private void Awake() {
        _transform = transform;
    }

    private void Update() {
        _transform.position = _collider.center;
    }

    [SerializeField]
    private WheelCollider _collider;
    private Transform _transform;
}
