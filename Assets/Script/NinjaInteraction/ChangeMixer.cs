using UnityEngine;
using UnityEngine.Audio;

public class ToggleMixer : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixerGroupA;
    [SerializeField] private AudioMixerGroup mixerGroupB;

    private bool usingA = true;

    public void ToggleAllAudioMixer()
    {
        AudioSource[] sources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (var src in sources)
        {
            src.outputAudioMixerGroup = usingA ? mixerGroupB : mixerGroupA;
        }

        usingA = !usingA;

        Debug.Log("Switched to: " + (usingA ? "Mixer A" : "Mixer B"));
    }
}
