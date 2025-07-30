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

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
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
            var targetPlayer = players.OrderBy(player => player.CurrentRank).FirstOrDefault();

            if (targetPlayer != null)
            {
                target = targetPlayer.transform;
                Debug.Log($"[Missile] Target acquired: {target.name}");
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
        if (other.CompareTag("Player"))
        {
            ApplyStunEffect(other.gameObject);
        }

   

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
        Gizmos.DrawWireSphere(transform.position, 5f); 
    }
}
