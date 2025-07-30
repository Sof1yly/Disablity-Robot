using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class BlueMissleScript : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 15f;
    public float rotateSpeed = 6f;
    public float lifeTime = 5f;

    [Header("Collision")]
    public float destroyDelay = 0.25f;

    private Rigidbody rb;
    private Transform target;
    private Collider col;

    private TrackUpdate[] players;

    [SerializeField] GameObject stunvfx;
    MeshRenderer mr;

    // Variable to track the shooter's rank
    private TrackUpdate shooter;

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
            // Get the shooter, assuming it's the player who launches the missile
            shooter = GetComponentInParent<TrackUpdate>();

            TrackUpdate targetPlayer = null;

            // If the shooter is ranked 1, target the second ranked player, else target the first ranked player
            if (shooter != null && shooter.CurrentRank == 1)
            {
                // Skip the shooter and get the second-ranked player
                targetPlayer = players.OrderBy(player => player.CurrentRank).Where(player => player != shooter) .FirstOrDefault();
            }
            else
            {
                // If the shooter is not ranked 1, target the first ranked player
                targetPlayer = players.OrderBy(player => player.CurrentRank).FirstOrDefault();
            }

            if (targetPlayer != null)
            {
                target = targetPlayer.transform;
                Debug.Log($"[Missile] Target acquired: {target.name} (Rank {targetPlayer.CurrentRank})");
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
            rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Ignore collision with the shooter
        if (other.gameObject == shooter.gameObject)
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
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
