using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BananaTrapTrigger : MonoBehaviour
{
    [Header("Spin Settings")]
    public float spinDuration = 1f;
    public float spinSpeed = 360f;

    public float destroyTime;

    bool used = false;

    [SerializeField] GameObject vfx;

    void OnTriggerEnter(Collider other)
    {
        if (used) return;

        used = true;

        if (other.TryGetComponent<StatusManage>(out StatusManage manage))
        {
            manage.OnApplyStatus(StatusType.Stun);
            vfx.SetActive(true);
        }
        Destroy(this.gameObject, destroyTime);
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
