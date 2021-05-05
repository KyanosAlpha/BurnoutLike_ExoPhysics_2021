using UnityEngine;

public class CarDeceleration : MonoBehaviour
{
    #region Unity API
    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update() 
    {
        GetInput();
    }

    private void FixedUpdate() 
    {
        Decelerate();
    }        

    /* private void OnDrawGizmos() {
        var rb = GetComponent<Rigidbody>();
        var t = transform;
        var velocityX = ApplyGrip(rb.velocity.x);
        var velocityZ = ApplyGrip(rb.velocity.z);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(t.position, t.position + t.right * velocityX);
        Gizmos.DrawLine(t.position, t.position + t.forward * velocityX);
    } */
    #endregion

    

    #region private methods
    private void Decelerate()
    {
        if(Mathf.Approximately(_input, 0) && !_rigidbody.IsSleeping())
        {
            var velocityX = ApplyGrip(_rigidbody.velocity.x);
            var velocityZ = ApplyGrip(_rigidbody.velocity.z);

            var newVelocity = new Vector3(velocityX, _rigidbody.velocity.y, velocityZ);
            _rigidbody.velocity = newVelocity;
        }
    }

    private float ApplyGrip(float velocityAxleComparer)
    {
        var velocityAxle = velocityAxleComparer;
        var gripAxle = velocityAxleComparer > 0 ? -1 : 1;
        velocityAxle -= gripAxle * _deceleration * Time.fixedDeltaTime;
        if(velocityAxle * gripAxle < 0){
            velocityAxle = 0;
        }

        return velocityAxle;
    }

    private void GetInput() => _input = Input.GetAxis("Vertical");        
    #endregion



    #region private members        
    private Rigidbody _rigidbody;
    private float _input;
    [SerializeField, Min(0)]
    private float _deceleration;
    #endregion
}
