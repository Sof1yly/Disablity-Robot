using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityScaler : MonoBehaviour
{
    public float gravityScale = 2f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // we’ll apply gravity ourselves
    }

    void FixedUpdate()
    {
        // apply scaled gravity each physics step
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
    }
}
