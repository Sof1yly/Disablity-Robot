using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Ability/SpeedBoost")]
public class SpeedBoost : ItemAbility
{
    [Tooltip("How many times faster the player goes")]
    public float boostMultiplier = 2f;
    [Tooltip("How long the boost lasts (seconds)")]
    public float boostDuration = 7f;

    public override void Activate(GameObject target)
    {
        // Attach the SpeedBoostEffect to the player at runtime
        var effect = target.AddComponent<SpeedBoostEffect>();
        effect.multiplier = boostMultiplier;
        effect.duration = boostDuration;
        Debug.Log($"Speed boost x{boostMultiplier} for {boostDuration}s applied to {target.name}");
    }
}
