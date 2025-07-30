using UnityEngine;
using System.Linq;
using DavidJalbert;

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
    [SerializeField] AudioClip missileHitSound;  // Sound to play when the missile hits a player
    [SerializeField] AudioClip runningSound;
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
            // Ensure the shooter is not targeted by the missile
            TrackUpdate targetPlayer = null;
            SoundPlayer.Instance.PlaySound(runningSound, 0);
            if (shooter != null && shooter.CurrentRank == 1)
            {
                // Skip the shooter and get the second-ranked player
                targetPlayer = players.OrderBy(player => player.CurrentRank).Where(player => player != shooter).FirstOrDefault();
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

        // Only apply the stun effect if the object has a StatusManage component
        if (other.TryGetComponent<StatusManage>(out StatusManage manage))
        {
            // Play the sound when the missile hits the player
            int playerIndex = other.transform.parent.GetComponentInChildren<TinyCarAudio>().player;
            SoundPlayer.Instance.PlaySound(missileHitSound, playerIndex);

            // Apply the stun effect and destroy the missile
            ApplyStunEffect(other.gameObject);
            Destroy(gameObject, destroyDelay);
        }
    }

    void ApplyStunEffect(GameObject player)
    {
        StatusManage statusManage = player.GetComponent<StatusManage>();
        if (statusManage != null)
        {
            statusManage.OnApplyStatus(StatusType.Stun);
        }
        mr.enabled = false;
        stunvfx.SetActive(true);  // Show the stun effect
        col.enabled = false;  // Disable the collider to prevent further triggers
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

    // Method to set the owner (shooter)
    public void SetOwner(GameObject owner)
    {
        shooter = owner.GetComponent<TrackUpdate>();
    }
}
