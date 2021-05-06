using UnityEngine;

public class VelocityViewer : MonoBehaviour
{
    public bool debug;
    private void OnDrawGizmos() {
        if(!debug) return;

        var t = transform;
        var velocity = transform.root.GetComponent<Rigidbody>().velocity;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(t.position, t.position + velocity);
    }
}
