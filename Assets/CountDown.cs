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
            TimerUpdate?.Invoke(Mathf.CeilToInt(i));
            Debug.Log(Mathf.CeilToInt(i));
            yield return null;
        }

        OnFinishCountDown?.Invoke();
    }
}
