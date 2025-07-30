using DavidJalbert;
using UnityEngine;
public class Stun : Status
{
    public override StatusType StatusType => StatusType.Stun;
    public override StatusType NextStatusType => StatusType.Iframe;


    [SerializeField] Rigidbody _rb;
    [SerializeField] TinyCarStandardInput input;
    [SerializeField] TinyCarController carController;
    [SerializeField] GameObject stunVFX;
    protected override void OnActive()
    {
        carController.enabled = false;
        input.enabled = false;
        _rb.linearVelocity = Vector3.zero;
        stunVFX.SetActive(true);
    }

    protected override void OffActive()
    {
        carController.enabled = true;
        input.enabled = true;
        stunVFX.SetActive(false);
    }
}
