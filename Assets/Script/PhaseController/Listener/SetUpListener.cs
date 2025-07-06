using System;
using UnityEngine;
using UnityEngine.Events;

public class SetUpListener : MonoBehaviour
{
    [SerializeField] UnityEvent OnEarlySetUp;
    [SerializeField] UnityEvent OnSetUp;
    public event Action OnEarlySetUpEvent;
    public event Action OnSetUpEvent;
    private void Awake()
    {
        FindAnyObjectByType<PhaseController>().EarlySetUp += earlySetUp;

        FindAnyObjectByType<PhaseController>().OnSetUpEvent += setUp;
    }

    void earlySetUp()
    {
        OnEarlySetUp?.Invoke();
        OnEarlySetUpEvent?.Invoke();
    }

    void setUp()
    {
        OnSetUp?.Invoke();
        OnSetUpEvent?.Invoke();
    }
}
