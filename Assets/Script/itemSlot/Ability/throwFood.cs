using UnityEngine;
using System.Collections;
using DavidJalbert;

[CreateAssetMenu(menuName = "Inventory/Ability/ThrowFood")]
public class throwFood : ItemAbility
{
    [SerializeField] private AudioClip powerSound;
    public override void Activate(GameObject target)
    {
        PlayThrowSound(target);
        var statusManage = target.GetComponent<StatusManage>();
        if (statusManage != null)
        {
            statusManage.OnApplyStatus(StatusType.VisualObscured);

        }
    }

    private void PlayThrowSound(GameObject target)
    {
        // Get the player's index to play the sound on the correct AudioSource
        int playerIndex = target.GetComponentInChildren<TinyCarAudio>().player;

        // Play the sound through the SoundPlayer
        SoundPlayer.Instance.PlaySound(powerSound, playerIndex);
    }
}
