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


    public void BeginCountDown()
    {
        onStartTimer?.Invoke();
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        for (float i = Timer; i > 0; i -= Time.deltaTime)
        {
            TimerUpdate?.Invoke(Mathf.CeilToInt(i));

            yield return null;
        }
        OnFinshCountDown?.Invoke();
        OnFinishCountDown?.Invoke();
    }
}
