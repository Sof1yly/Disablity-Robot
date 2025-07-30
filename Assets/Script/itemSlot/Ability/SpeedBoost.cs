using UnityEngine;
using UnityEngine.InputSystem.XR;
using DavidJalbert;

[CreateAssetMenu(menuName = "Inventory/Ability/SpeedBoost")]
public class SpeedBoost : ItemAbility
{
    public float effectDuration = 3f;


    public TinyCarSurfaceParameters effectParameters = new TinyCarSurfaceParameters();
    public override void Activate(GameObject target)
    {

        DavidJalbert.TinyCarController carController = target.GetComponent<DavidJalbert.TinyCarController>();
        if (carController != null)
        {
            // Pass the directly defined effectParameters to the car controller
            carController.ApplyTemporarySurfaceEffect(effectParameters, effectDuration);
        }


    }
}
