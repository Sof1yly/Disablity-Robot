using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class BlueMissleScript : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 15f;
    public float rotateSpeed = 6f;
    public float lifeTime = 5f;

    [Header("Target-finding")]
    public float detectionRadius = 5f;

    [Header("Collision")]
    public float destroyDelay = 0.25f;

    [HideInInspector]
    public GameObject owner;                

    private Rigidbody rb;
    private Transform target;
    private Collider col;
    private TrackUpdate[] players;
    private TrackUpdate ownerTrack;

    //ajarn Born

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        Destroy(gameObject, lifeTime);
    }

    void Start()
    {
        // Cache all players (TrackUpdate) and the owner's track component
        players = Object.FindObjectsByType<TrackUpdate>(FindObjectsSortMode.None);
        if (owner != null)
            ownerTrack = owner.GetComponent<TrackUpdate>();
    }

    void Update()
    {
        if (target == null)
        {
            // Sort by CurrentRank ascending 
            var sorted = players.OrderBy(p => p.CurrentRank).ToList();
            if (sorted.Count == 0)
                return;

            // If owner is first-ranked, target the second
            if (ownerTrack != null && ownerTrack.CurrentRank == 1 && sorted.Count > 1)
            {
                target = sorted[1].transform;
            }
            else
            {
                // Otherwise target the leader (first in list), unless that is the owner
                var leader = sorted[0];
                if (leader != ownerTrack)
                    target = leader.transform;
                else if (sorted.Count > 1)
                    target = sorted[1].transform;
            }

            if (target != null)
                Debug.Log($"[Missile] Target acquired: {target.name}");
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            Quaternion look = Quaternion.LookRotation(dir);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, look, rotateSpeed * Time.fixedDeltaTime));
            rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Ignore collisions with the owner
        if (owner != null && other.gameObject == owner)
            return;

        // Apply stun if the hit object has a StatusManage
        if (other.TryGetComponent<StatusManage>(out var manage))
        {
            manage.OnApplyStatus(StatusType.Stun);
        }

        // Destroy missile after delay
        Destroy(gameObject, destroyDelay);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
