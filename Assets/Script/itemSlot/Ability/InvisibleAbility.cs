using System.Collections;
using System.Xml.Schema;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory/Ability/Invisible")]
public class InvisibleAbility :ItemAbility
{
    [SerializeField] private float invincibleDuration = 5f;
    public override void Activate(GameObject target)
    {
        StatusManage statusManage = target.GetComponent<StatusManage>();
        if(statusManage != null)
        {
            statusManage.OnApplyStatus(StatusType.Invincible);

            var targetMono = target.GetComponent<MonoBehaviour>();
            if(targetMono != null)
            {
                targetMono.StartCoroutine(DeactivateInvi(statusManage));
            }

            
        }
    }

    private IEnumerator DeactivateInvi(StatusManage statusManage)
    {
        yield return new WaitForSeconds(invincibleDuration);

        statusManage.DisactiveStatus(StatusType.Invincible);
    }

  

}
