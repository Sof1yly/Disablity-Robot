using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class CountDown : MonoBehaviour
{
    [SerializeField] float Timer;
    [SerializeField] UnityEvent OnFinishCountDown;
    [SerializeField] UnityEvent<int> TimerUpdate;
    public void BeginCountDown()
    {
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        for (float i = Timer; i > 0; i -= Time.deltaTime)
        {
            TimerUpdate?.Invoke(Mathf.CeilToInt(Timer));
            Debug.Log(Mathf.CeilToInt(Timer));
            yield return null;
        }

        OnFinishCountDown?.Invoke();
    }
}
