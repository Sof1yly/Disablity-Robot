using UnityEngine;

public class PhaseUpdateMinigameState_Caller : MonoBehaviour
{
    PhaseController phaseController;

    private void Awake()
    {
        phaseController = FindAnyObjectByType<PhaseController>();
    }

    public void UpdateMinigameWinState(bool State)
    {
        phaseController.UpdateMinigameState(State);
    }

    public void UpdateStateAndGoNextPhase(bool State)
    {
        phaseController.UpdateMinigameState(State);
        phaseController.NextStage();
    }

}
