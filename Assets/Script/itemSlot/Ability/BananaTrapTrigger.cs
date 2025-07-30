using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BananaTrapTrigger : MonoBehaviour
{
    private SoundPlayer soundPlayer;
    bool used = false;

    void OnTriggerEnter(Collider other)
    {
 
        if (used || !other.TryGetComponent<StatusManage>(out StatusManage manage))
        {
            return;
        }
   
        used = true;
        soundPlayer.PlaySound(SoundPlayer.Instance.Sources[0].clip, 0); 
        manage.OnApplyStatus(StatusType.Stun);
        Debug.Log($"Banana trap stunned {other.name}");

        Destroy(gameObject);
    }
}
