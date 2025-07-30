using DavidJalbert;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Ability/CallTeacher")]
public class CallTeach : ItemAbility
{
    public GameObject teacherPrefab;

    public int teacherCount = 1;

    public Vector3 spawnOffset = new Vector3(2, 6, 0);
    [SerializeField] private AudioClip powerSound;

    public override void Activate(GameObject user)
    {
        Vector3 origin = user.transform.position + spawnOffset;
        Quaternion rot = user.transform.rotation;
        for (int i = 0; i < teacherCount; i++)
        {
            PlayThrowSound(user);
            Instantiate(teacherPrefab, origin, rot);
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
