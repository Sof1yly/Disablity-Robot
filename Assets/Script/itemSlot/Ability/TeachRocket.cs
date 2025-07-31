using DavidJalbert;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TeachRocket : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 20f;
    public float lifeTime = 5f;

    [SerializeField] AudioClip rocketHitSound;  // Sound when rocket hits a player
    [SerializeField] AudioClip runningSound;

    private Rigidbody rb;
    private Collider col;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        Destroy(gameObject, lifeTime); // Destroy the rocket after lifetime
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed;  // Give the rocket an initial velocity
        SoundPlayer.Instance.PlaySound(runningSound, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        // Only apply the stun effect if the object has a StatusManage component
        if (other.TryGetComponent<StatusManage>(out var manage))
        {
            // Play sound when the rocket hits a valid target
            int playerIndex = other.transform.parent.GetComponentInChildren<TinyCarAudio>().player;
            SoundPlayer.Instance.PlaySound(rocketHitSound, playerIndex);

            ApplyStunEffect(other.gameObject);
        }
    }

    void ApplyStunEffect(GameObject player)
    {
        StatusManage statusManage = player.GetComponent<StatusManage>();
        if (statusManage != null)
        {
            statusManage.OnApplyStatus(StatusType.Stun);  // Apply the stun status
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);  // Display a gizmo for the rocket's collision radius
    }
}
