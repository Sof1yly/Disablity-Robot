using DavidJalbert;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BananaTrapTrigger : MonoBehaviour
{
    bool used = false;

    [SerializeField] private AudioClip bookTrapSound;
    [SerializeField] private GameObject stunvfx; // Stun effect
    [SerializeField] private float destroyDelay = 0.25f; // Time before the trap is destroyed

    private void Awake()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        // Avoid triggering the trap multiple times
        if (used || !other.TryGetComponent<StatusManage>(out StatusManage manage))
        {
            return;
        }

        used = true;

        // Play sound for the player who triggered the trap
        int playerIndex = other.transform.parent.GetComponentInChildren<TinyCarAudio>().player;
        SoundPlayer.Instance.PlaySound(bookTrapSound, playerIndex);

        // Apply the stun effect
        manage.OnApplyStatus(StatusType.Stun);
        Debug.Log($"Banana trap stunned {other.name}");

        stunvfx.SetActive(true);

        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, destroyDelay);
    }
}
