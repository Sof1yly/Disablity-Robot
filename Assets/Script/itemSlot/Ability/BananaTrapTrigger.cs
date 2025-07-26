using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class BananaTrapTrigger : MonoBehaviour
{
    bool used = false;
    Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = false;
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (used) return;

        var other = collision.collider;
        if (other.TryGetComponent<StatusManage>(out var manage))
        {
            used = true;
            manage.OnApplyStatus(StatusType.Stun);
            Destroy(gameObject);
            Debug.Log($"Banana trap stunned {other.name}");
        }
    }
}
