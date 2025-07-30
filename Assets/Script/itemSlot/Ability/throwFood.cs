using UnityEngine;
using System.Collections;
using DavidJalbert;

[CreateAssetMenu(menuName = "Inventory/Ability/ThrowFood")]
public class throwFood : ItemAbility
{
    public override void Activate(GameObject target)
    {
        var statusManage = target.GetComponent<StatusManage>();
        if (statusManage != null)
        {
            statusManage.OnApplyStatus(StatusType.VisualObscured);

        }
    }

}
