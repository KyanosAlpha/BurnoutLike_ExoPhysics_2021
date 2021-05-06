using UnityEngine;

//TODO: souci de friction apd moment où on tourne à cause de la mécanique de rotation du rb... :shrug:

public class TireFriction : MonoBehaviour
{
    #region private fields
    private Transform _transform;       
    private Rigidbody _rigidbody;
    [Header("Forward friction")]
    [SerializeField]
    private float _minimalForwardGrip;
    [SerializeField]
    private float _additionalForwardGrip;
    [Header("Sideway friction")]
    [SerializeField]
    private float _minimalSidewayGrip;
    [SerializeField]
    private float _additionalSidewayGrip;
    #endregion



    #region unity api
    private void Awake() 
    {
        _rigidbody = transform.root.GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void FixedUpdate() 
    {
        var velocity = _rigidbody.velocity;
        var tmpVelocity = new Vector3(velocity.x, 0, velocity.z);
        var angularity = tmpVelocity.sqrMagnitude > 0 ? Vector3.Dot(_transform.right, tmpVelocity.normalized) : 0f;
        angularity = Mathf.Abs(angularity);

        var velocityX = CalculateFriction(velocity.x, 1, _minimalForwardGrip, _additionalForwardGrip);
        var velocityZ = CalculateFriction(velocity.z, 1, _minimalSidewayGrip, _additionalSidewayGrip);
        var newVelocity = new Vector3(velocityX, velocity.y, velocityZ);

        _rigidbody.velocity = newVelocity;
    }       

    /* private void OnGUI() {
        var velocity = transform.root.GetComponent<Rigidbody>().velocity;
        var angularity = Vector3.Dot(_transform.right, _rigidbody.velocity.normalized);
        angularity = Mathf.Abs(angularity);

        var velocityX = CalculateFriction(velocity.x, angularity);
        var velocityZ = CalculateFriction(velocity.z, angularity);

        GUILayout.Button($"angularity : {angularity : 0.00}");
        GUILayout.Button($"velocity : {velocity : 0.00}");
        GUILayout.Button($"x : {velocityX : 0.00}");
        GUILayout.Button($"z : {velocityZ : 0.00}");
    } */
    #endregion

    

    #region private method
    private float CalculateFriction(float axleVelocity, float angularity, float minimalGrip, float additionalGrip)
    {
        var normalizedAxle = axleVelocity > 0 ? 1f : axleVelocity < 0 ? -1f : 0;
        var friction = normalizedAxle * (additionalGrip * angularity + minimalGrip);
        friction *= Time.fixedDeltaTime;
        axleVelocity -= friction;

        if(axleVelocity * normalizedAxle <= 0)
        {
            axleVelocity = 0;
        }

        return axleVelocity;
    }        
    #endregion
}