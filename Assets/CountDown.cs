using MoreMountains.Tools;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class CountDown : MonoBehaviour
{
    [SerializeField] float Timer;
    public event Action OnFinshCountDown;
    [SerializeField] UnityEvent OnFinishCountDown;
    public event Action onStartTimer;
    public event Action<int> TimerUpdate;
    [SerializeField] AudioClip sfx321;
    [SerializeField] AudioClip sfxGo;


    public void BeginCountDown()
    {
        onStartTimer?.Invoke();
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        int e = 0;
        for (float i = Timer; i > 0; i -= Time.deltaTime)
        {
            if(e != Mathf.CeilToInt(i))
            {
                e = Mathf.CeilToInt(i);
                TimerUpdate?.Invoke(Mathf.CeilToInt(i));
                SoundPlayer.Instance.PlaySound(sfx321, 0);
            }

            yield return null;
        }
        SoundPlayer.Instance.PlaySound(sfxGo, 0);
        OnFinshCountDown?.Invoke();
        OnFinishCountDown?.Invoke();
    }
}
