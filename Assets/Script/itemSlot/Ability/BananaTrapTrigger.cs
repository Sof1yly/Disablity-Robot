using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BananaTrapTrigger : MonoBehaviour
{
    bool used = false;
    Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();

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
