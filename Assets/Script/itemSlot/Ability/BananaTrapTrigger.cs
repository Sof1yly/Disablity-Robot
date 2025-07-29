using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BananaTrapTrigger : MonoBehaviour
{
    bool used = false;

    void OnTriggerEnter(Collider other)
    {
 
        if (used || !other.TryGetComponent<StatusManage>(out StatusManage manage))
        {
            return;
        }
   
        used = true;
        manage.OnApplyStatus(StatusType.Stun);
        Debug.Log($"Banana trap stunned {other.name}");

        Destroy(gameObject);
    }
}

//yhfgh
