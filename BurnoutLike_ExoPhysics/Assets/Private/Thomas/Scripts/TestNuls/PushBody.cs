using UnityEngine;

public class PushBody : MonoBehaviour
{
    [Range(-1, 1)]
    public float sense = 0;
    public float factor = 10; 
    public Rigidbody rb;
    private void FixedUpdate() {
        rb.AddForce(transform.forward * sense * factor, ForceMode.Acceleration);
    }
}
