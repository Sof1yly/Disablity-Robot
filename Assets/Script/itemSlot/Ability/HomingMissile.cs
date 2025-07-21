using UnityEngine;
using System.Collections;

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
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (var h in hits)
            {
                if (h.CompareTag(targetTag))
                {
                    target = h.transform;
                    Debug.Log($"[Missile] target acquired: {target.name}");
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
        if (other.CompareTag(targetTag))
        {
            other.transform.Rotate(0f, 80f, 0f, Space.Self);
            Debug.Log("[Missile] Hit and spun player");
        }

   
        if (col != null) col.enabled = false;


        Destroy(gameObject, destroyDelay);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
