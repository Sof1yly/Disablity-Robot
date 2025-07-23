using UnityEngine;

public class CarKnockBack : MonoBehaviour
{
    public float knockbackForce = 10f;
    private string Obstruct = "Obstruct";
    public float knockbackCooldown = 0.5f;

    private float lastKnockbackTime = -999f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 0f; // optional: limit spin
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastKnockbackTime < knockbackCooldown) return;
        if (!collision.collider.CompareTag(Obstruct)) return;
        // Knockback direction = contact normal, but flattened
        Vector3 rawNormal = collision.contacts[0].normal;
        Vector3 horizontalNormal = new Vector3(rawNormal.x, 0f, rawNormal.z).normalized;

        // Apply instant push using VelocityChange
        rb.AddForce(horizontalNormal * knockbackForce, ForceMode.VelocityChange);

        // Remove unwanted spin
        rb.angularVelocity = Vector3.zero;

        lastKnockbackTime = Time.time;
    }
}
