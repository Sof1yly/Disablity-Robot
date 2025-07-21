using System.Collections;
using UnityEngine;

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

        if (other.TryGetComponent<StatusManage>(out StatusManage manage))
        {
            manage.OnApplyStatus(StatusType.Stun);
        }
        Destroy(this.gameObject);
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
