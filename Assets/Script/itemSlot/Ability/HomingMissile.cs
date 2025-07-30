using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 15f;
    public float rotateSpeed = 6f;
    public float lifeTime = 5f;

    [Header("Target-finding")]
    public float detectionRadius = 80f;

    [Header("Collision")]
    public float destroyDelay = 0.25f;

    [HideInInspector] public GameObject owner;  // Set by the spawner

    private Rigidbody rb;
    private Transform target;
    private Collider col;

    private TrackUpdate[] players;

    [SerializeField] GameObject stunvfx;
    MeshRenderer mr;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        mr = GetComponent<MeshRenderer>();
        Destroy(gameObject, lifeTime);
    }

    void Start()
    {
        players = Object.FindObjectsByType<TrackUpdate>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (target == null)
        {
            // Look for players to target in the detection radius
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (var h in hits)
            {
                if (h.TryGetComponent<StatusManage>(out var sm))
                {
                    // Ignore the shooter (the one who fired the missile)
                    if (h.gameObject == owner)
                        continue;

                    // Set the first valid target
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
        // Ignore collision with the shooter
        if (other.gameObject == owner)
            return;

        ApplyStunEffect(other.gameObject);
        Destroy(gameObject, destroyDelay);
    }

    void ApplyStunEffect(GameObject player)
    {
        StatusManage statusManage = player.GetComponent<StatusManage>();
        if (statusManage != null)
        {
            statusManage.OnApplyStatus(StatusType.Stun);
        }
        mr.enabled = false;
        stunvfx.SetActive(true);
        col.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    // Method to set the owner (shooter)
    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }
}
