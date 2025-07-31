using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Ability/ThrowFood")]
public class ThrowFood : ItemAbility
{
    [Tooltip("How long the visual obscuration lasts")]
    public float duration = 2f;

    public override void Activate(GameObject user)
    {
        // Find every StatusManage in the scene (one per player)
        var allStatus = Object.FindObjectsByType<StatusManage>(FindObjectsSortMode.None);

        foreach (var sm in allStatus)
        {
            // skip the user themselves
            if (sm.gameObject == user)
                continue;

            // apply the status to everyone else
            sm.OnApplyStatus(StatusType.VisualObscured);
        }
    }
}
