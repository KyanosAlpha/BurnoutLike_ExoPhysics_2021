using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    public delegate void Check();
    public event Check CheckEvent;

    private void OnTriggerEnter(Collider other) {
        Validate();
    }

    private void Validate()
    {
        CheckEvent.Invoke();
        gameObject.SetActive(false);
    }
}
