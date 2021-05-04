using UnityEngine;

public class WheelGizmos : MonoBehaviour
{
    private void OnDrawGizmosSelected() {
        WheelCollider wheel = GetComponent<WheelCollider>();
        Gizmos.color = Color.red;
        var pos = Vector3.zero;
        var rot = Quaternion.identity;
        wheel.GetWorldPose(out pos, out rot);
        Gizmos.DrawSphere(pos, 0.05f);
    }
}
