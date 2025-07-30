using DavidJalbert;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Ability/HomingMissile")]
public class HomingMissileAbility : ItemAbility
{
    [Tooltip("Prefab with the HomingMissile component")]
    public GameObject missilePrefab;

    [Tooltip("How many to fire when used")]
    public int missileCount = 2;

    [Tooltip("Local offset from user when spawning")]
    public Vector3 spawnOffset = new Vector3(0, 1, 0);

    [SerializeField] private AudioClip powerSound;

    public override void Activate(GameObject user)
    {
        Vector3 origin = user.transform.position + spawnOffset;
        Quaternion rot = user.transform.rotation;
        for (int i = 0; i < missileCount; i++)
        {
            GameObject missile = Instantiate(missilePrefab, origin, rot);
            PlayThrowSound(user);
            HomingMissile missileScript = missile.GetComponent<HomingMissile>();
            if (missileScript != null)
            {
                missileScript.SetOwner(user);
            }
        }
        Debug.Log($"Spawned {missileCount} homing missiles");
    }

    private void PlayThrowSound(GameObject user)
    {
        // Get the player's index to play the sound on the correct AudioSource
        int playerIndex = user.GetComponentInChildren<TinyCarAudio>().player;

        // Play the sound through the SoundPlayer
        SoundPlayer.Instance.PlaySound(powerSound, playerIndex);
    }
}
