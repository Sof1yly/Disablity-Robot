using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BananaTrapTrigger : MonoBehaviour
{
    bool used = false;
    [SerializeField] GameObject vfx;

    void OnTriggerEnter(Collider other)
    {
 
        if (used || !other.TryGetComponent<StatusManage>(out StatusManage manage))
        {
            return;
        }
   
        used = true;
        manage.OnApplyStatus(StatusType.Stun);
        Debug.Log($"Banana trap stunned {other.name}");
        vfx.SetActive(true);
        Destroy(gameObject);
    }
}