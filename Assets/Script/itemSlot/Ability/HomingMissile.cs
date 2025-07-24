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
    public string targetTag = "Player";

    [Header("Collision")]
    public float destroyDelay = 0.25f;

    private Rigidbody rb;
    private Transform target;
    private Collider col;

    [SerializeField] GameObject stunvfx;
    MeshRenderer mr;

    bool isDead = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        mr = GetComponent<MeshRenderer>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Look for target if not already acquired
        if (target == null)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (var h in hits)
            {
                if (h.CompareTag(targetTag))
                {
                    target = h.transform;
                    Debug.Log($"[Missile] Target acquired: {target.name}");
                    break;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            if (target != null)
            {
                Vector3 dir = (target.position - transform.position).normalized;
                Quaternion look = Quaternion.LookRotation(dir);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, look, rotateSpeed * Time.fixedDeltaTime));
            }
            rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {

            ApplyStunEffect(other.gameObject);
            Debug.Log("[Missile] Hit and stunned the player");
        }
        isDead = true;
        mr.enabled = false;
        stunvfx.SetActive(true);
        Destroy(gameObject, destroyDelay);
    }


    void ApplyStunEffect(GameObject player)
    {

        StatusManage statusManage = player.GetComponent<StatusManage>();
        if (statusManage != null)
        {
            statusManage.OnApplyStatus(StatusType.Stun);
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
