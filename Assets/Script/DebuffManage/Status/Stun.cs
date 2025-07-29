using DavidJalbert;
using UnityEngine;
public class Stun : Status
{
    public override StatusType StatusType => StatusType.Stun;
    public override StatusType NextStatusType => StatusType.Iframe;


    [SerializeField] Rigidbody _rb;
    [SerializeField] TinyCarStandardInput input;
    [SerializeField] TinyCarController carController;
    [SerializeField] GameObject vfxStun;
    protected override void OnActive()
    {
        carController.enabled = false;
        vfxStun.SetActive(true);
        input.enabled = false;
        _rb.linearVelocity = Vector3.zero;
    }

    protected override void OffActive()
    {
        carController.enabled = true;
        vfxStun.SetActive(false);
        input.enabled = true;
    }
}
