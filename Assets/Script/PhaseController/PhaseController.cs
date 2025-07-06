using System;
using System.Linq;
using UnityEngine;

public enum Phase
{
    Intermission, Minigame, EndMinigame
}
public class PhaseController : MonoBehaviour
{
    public event Action EarlySetUp;
    public event Action OnSetUpEvent;
    public event Action<Phase> PhaseUpdateEvent;
    public event Action<bool> MiniGameStateUpdateEvent;

    Phase phase = Phase.Intermission;
    bool MinigameState;
    private void Start()
    {
        EarlySetUp?.Invoke();
        OnSetUpEvent?.Invoke();
        PhaseUpdateEvent.Invoke(phase);
    }
    [ContextMenu("Next Stage Text")]
    public void NextStage()
    {
        Debug.Log("Call Method [NextStage]");
        int maxValue = Enum.GetValues(typeof(Phase)).Cast<int>().Max();
        int currentPhaseInt = (int)phase;

        if (currentPhaseInt < maxValue)
        {
            phase = (Phase)(currentPhaseInt + 1);
            PhaseUpdateEvent.Invoke(phase);
        }
        else
        {
            Debug.Log("Game End Go back to Da something Scene");
            // Just for Test
            // Loading Next Scene Here
        }
    }
    public void UpdateMinigameState(bool UpdateState) // Calling this when Minigame End
    {
        MinigameState = UpdateState;
        MiniGameStateUpdateEvent?.Invoke(UpdateState);
    }


}
