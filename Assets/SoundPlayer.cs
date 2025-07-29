using MoreMountains.Tools;
using UnityEngine;
using static MoreMountains.Feedbacks.MMF_Sound;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance; private void Awake() { if (Instance != null) Destroy(this.gameObject); else DontDestroyOnLoad(this.gameObject); Instance = this; }

    public AudioSource[] Sources;

    /// <summary>
    /// Plays sound: AudioClip, Speaker number
    /// </summary>
    /// <param name="sfx"></param>
    /// <param name="source"></param>
    public void PlaySound(AudioClip sfx, int source)
    {
        MMSoundManagerPlayOptions options;

        options = MMSoundManagerPlayOptions.Default;

        options.SpatialBlend = 1;
        options.Location = Sources[source--].transform.position;

        MMSoundManagerSoundPlayEvent.Trigger(sfx, options);
    }
}
