using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 15f;
    public float rotateSpeed = 6f;
    public float lifeTime = 5f;

    [Header("Target-finding")]
    public float detectionRadius = 5f;

    [Header("Collision")]
    public float destroyDelay = 0.25f;

    [HideInInspector] public GameObject owner;  // set by the spawner

    Rigidbody rb;
    Transform target;
    Collider col;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (target == null)
        {
            // find anything with a StatusManage
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (var h in hits)
            {
                if (h.TryGetComponent<StatusManage>(out var sm))
                {
                    // ignore the shooter
                    if (h.gameObject == owner)
                        continue;

                    target = h.transform;
                    Debug.Log($"[Missile] Target acquired: {target.name}");
                    break;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            Quaternion look = Quaternion.LookRotation(dir);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, look, rotateSpeed * Time.fixedDeltaTime));
        }
        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // ignore hits on the shooter
        if (other.gameObject == owner)
            return;


        if (other.TryGetComponent<StatusManage>(out var manage))
        {
            manage.OnApplyStatus(StatusType.Stun);
            Debug.Log($"[Missile] Stunned {other.name}");
        }

        col.enabled = false;  // prevent re-entry
        Destroy(gameObject, destroyDelay);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
