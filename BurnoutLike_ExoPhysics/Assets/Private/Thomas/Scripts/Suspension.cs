using UnityEngine;

public class Suspension : MonoBehaviour
{
    #region Unity API
    private void Awake() {
        _transform = transform;
    }

    private void Update() {
        UpdateGraphics();
    }   
    #endregion



    #region private methods
    private void UpdateGraphics()
    {
        var position = Vector3.zero;
        var rotation = Quaternion.identity;
        _collider.GetWorldPose(out position, out rotation);

        _transform.position = position;
        _transform.rotation = rotation;
    } 
    #endregion



    #region private fields
    [SerializeField]
    private WheelCollider _collider;
    private Transform _transform;        
    #endregion
}
