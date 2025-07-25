using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TeachRocket : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 20f;  
    public float lifeTime = 5f;   


    private Rigidbody rb;
    private Collider col;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        Destroy(gameObject, lifeTime); 
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed;  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyStunEffect(other.gameObject);


        }

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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);  
    }
}
