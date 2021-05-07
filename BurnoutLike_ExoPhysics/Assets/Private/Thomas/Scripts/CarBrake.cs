using UnityEngine;

public class CarBrake : MonoBehaviour
{
    #region inspector
    [SerializeField, Min(0)]
    private float _carBrakeIntensity;
    [SerializeField]
    private bool _debug;
    #endregion



    #region private fields
    private bool _input;
    private CarController _controller;        
    #endregion



    #region unity messages
    private void Awake()
    {
        _controller = transform.root.GetComponent<CarController>();
        if(_controller == null)
        {
            Debug.LogError("No CarController found on the root of this gameObject", this);
        }
    }

    private void FixedUpdate()
    {
        Carbrake();
    }

    private void Update() {
        UpdateInput();
    }

    private void OnDrawGizmos() {
        if(!_debug || _controller == null) return;

        var t = transform;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(t.position, t.position - _controller.CarRigidbody.velocity.normalized * _carBrakeIntensity * (_input ? 1 : 0) * Time.fixedDeltaTime);
    }
    #endregion



    #region private methods
    private void UpdateInput() => _input = Input.GetKey(KeyCode.A);

    private void Carbrake()
    {
        if (!(_input && _controller.Grounded)) return;

        var velocity = _controller.CarRigidbody.velocity;
        var brake = velocity.normalized * _carBrakeIntensity * Time.fixedDeltaTime;

        var deltaX = ApplyBrake(velocity.x, brake.x);
        var deltaY = ApplyBrake(velocity.y, brake.y);
        var deltaZ = ApplyBrake(velocity.z, brake.z);

        Vector3 newVelocity = new Vector3(deltaX, deltaY, deltaZ);
        Debug.Log(newVelocity);
        _controller.CarRigidbody.velocity = newVelocity;
    }

    public float ApplyBrake(float velocity, float brake){
        var delta = velocity - brake;
        if (delta * velocity > 0) return delta;
        Debug.Log(0);
        
        return 0;
    }
    #endregion
}