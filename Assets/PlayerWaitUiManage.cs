using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWaitUiManage : MonoBehaviour
{
    [Header("Set Up")]
    [SerializeField] PlayerCheck Player;
    [SerializeField] CountDown timer;
    [SerializeField] UnityEvent OnStart;
    [Header("Wait Player Input Phase")]
    [SerializeField] UnityEvent PlayerNotPressButtonYet;
    [SerializeField] UnityEvent PlayerPressButton;
    [SerializeField] UnityEvent OnFinishWaitPlayerPhase;

    [Header("Count Down Phase")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] UnityEvent OnStartTimerPhase;
    [SerializeField] UnityEvent OnFinishTimer;
    private void Awake()
    {
        if (timer == null)
        {
            timer = FindAnyObjectByType<CountDown>();
        }

        Player.StateUpdater += playerInputStateHolder;
        timer.onStartTimer += OnStartTimer;
        timer.TimerUpdate += TimerUpdate;
        timer.OnFinshCountDown += onFinishTimer;
    }

    private void Start()
    {
        OnStart?.Invoke();
    }
    void playerInputStateHolder(bool isPress)
    {
        if (isPress)
        {
            PlayerPressButton?.Invoke();
        }
        else
        {
            PlayerNotPressButtonYet?.Invoke();
        }
    }


    public void OnStartTimer()
    {
        OnFinishWaitPlayerPhase?.Invoke();
        OnStartTimerPhase?.Invoke();
    }

    public void TimerUpdate(int Timer)
    {
        timerText.text = Timer.ToString();
    }
    void onFinishTimer()
    {
        OnFinishTimer?.Invoke();
    }

}
