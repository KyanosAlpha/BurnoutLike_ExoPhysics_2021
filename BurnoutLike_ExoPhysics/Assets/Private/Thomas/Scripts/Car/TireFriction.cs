using UnityEngine;

//TODO: souci de friction apd moment où on tourne à cause de la mécanique de rotation du rb... :shrug:

public class TireFriction : MonoBehaviour
{
    #region private fields
    private CarController _controller;
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
        _controller = transform.root.GetComponent<CarController>();
        if(_controller == null)
        {
            Debug.LogError("No CarController found on the root of this gameObject", this);
        }
    }

    private void FixedUpdate() 
    {
        ApplyTireFriction();
    }

    private void ApplyTireFriction()
    {
        if(!_controller.Grounded) return;

        var velocity = _controller.CarRigidbody.velocity;
        var angularity = Vector3.Dot(_controller.CarTransform.right, velocity.normalized);
        angularity = Mathf.Abs(angularity);

        var velocityX = CalculateFriction(velocity.x, angularity, _minimalForwardGrip, _additionalForwardGrip);
        //var velocityY = CalculateFriction(velocity.y, angularity, _minimalForwardGrip, _additionalForwardGrip);
        var velocityZ = CalculateFriction(velocity.z, angularity, _minimalSidewayGrip, _additionalSidewayGrip);
        var newVelocity = new Vector3(velocityX, velocity.y, velocityZ);

        _controller.CarRigidbody.velocity = newVelocity;
    }
    #endregion



    #region private method
    private float CalculateFriction(float axleVelocity, float angularity, float minimalGrip, float additionalGrip)
    {
        var normalizedAxle = axleVelocity > 0 ? 1f : -1f; //axleVelocity < 0 ? -1f : 0;
        var friction = normalizedAxle * (additionalGrip * angularity + minimalGrip);
        friction *= Time.fixedDeltaTime;
        axleVelocity -= friction;

        if(axleVelocity * normalizedAxle > 0) return axleVelocity;
        
        return 0;
    }        
    #endregion
}