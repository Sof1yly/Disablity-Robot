using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

public class PlayFinishUI : MonoBehaviour
{
    [SerializeField] UnityEvent MM_Fader;
    private void OnEnable()
    {
        MM_Fader.Invoke();
        MMFaderDirectional.Destroy(this);
    }
}
