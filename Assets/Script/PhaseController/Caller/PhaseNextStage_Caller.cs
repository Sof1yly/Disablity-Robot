using UnityEngine;

public class PhaseNextStage_Caller : MonoBehaviour
{
    PhaseController phaseController;

    private void Awake()
    {
        phaseController = FindAnyObjectByType<PhaseController>();
    }

    public void PhaseGoNextPhase()
    {
        phaseController.NextStage();
    }

}
