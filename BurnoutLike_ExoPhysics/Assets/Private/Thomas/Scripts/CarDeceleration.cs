using UnityEngine;

public class CarDeceleration : MonoBehaviour
{
    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        Decelerate();
    }

    private void Decelerate()
    {
        if(Mathf.Approximately(_input, 0) && !_rigidbody.IsSleeping())
        {
            _rigidbody.velocity *= 1 - (1 / _deceleration);
        }
    }

    private void Update() 
    {
        GetInput();
    }

    private void GetInput() => _input = Input.GetAxis("Vertical");

    private Rigidbody _rigidbody;
    private float _input;
    [SerializeField, Min(1)]
    private float _deceleration;
}
