using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BananaTrapTrigger : MonoBehaviour
{
    [Header("Spin Settings")]
    public float spinDuration = 1f;
    public float spinSpeed = 360f;

    bool used = false;

    void OnTriggerEnter(Collider other)
    {
        if (used) return;

        used = true;
        StartCoroutine(SpinCoroutine(other.transform));
        Destroy(gameObject, spinDuration + 0.1f);
    }

    IEnumerator SpinCoroutine(Transform t)
    {
        float elapsed = 0f;
        while (elapsed < spinDuration)
        {
            t.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.Self);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
