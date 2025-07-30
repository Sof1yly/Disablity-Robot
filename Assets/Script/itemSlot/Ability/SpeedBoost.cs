using UnityEngine;
using UnityEngine.InputSystem.XR;
using DavidJalbert;

[CreateAssetMenu(menuName = "Inventory/Ability/SpeedBoost")]
public class SpeedBoost : ItemAbility
{
    public float effectDuration = 3f;

    [SerializeField] private AudioClip powerSound;

    public TinyCarSurfaceParameters effectParameters = new TinyCarSurfaceParameters();
    public override void Activate(GameObject target)
    {
        PlayThrowSound(target);
        DavidJalbert.TinyCarController carController = target.GetComponent<DavidJalbert.TinyCarController>();
        if (carController != null)
        {
            // Pass the directly defined effectParameters to the car controller
            carController.ApplyTemporarySurfaceEffect(effectParameters, effectDuration);
        }


    }

    private void PlayThrowSound(GameObject user)
    {
        // Get the player's index to play the sound on the correct AudioSource
        int playerIndex = user.GetComponentInChildren<TinyCarAudio>().player;

        // Play the sound through the SoundPlayer
        SoundPlayer.Instance.PlaySound(powerSound, playerIndex);
    }
}
