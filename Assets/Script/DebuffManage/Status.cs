using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Status : MonoBehaviour
{
    [SerializeField] float _debuffTime;
    [SerializeField] List<StatusType> _inharmoniousStatus = new List<StatusType>();
    public List<StatusType> InharmoniousStatus => _inharmoniousStatus;
    public virtual StatusType StatusType => StatusType.None; // Declear status type here
    public virtual StatusType NextStatusType => StatusType.None; // None => NO apply any status after this Status End 
    protected abstract void OnActive();
    protected abstract void OffActive();


    StatusManage statusManage;
    public void AddStatusManage(StatusManage addedStatusManage = null)
    {
        statusManage = addedStatusManage;
    }

    public void ApplyEffect()
    {
        StartCoroutine(statusTimer());
    }

    IEnumerator statusTimer()
    {
        OnActive();
        yield return new WaitForSeconds(_debuffTime);
        OffActive();
        disactiveSelf();
        applyNextStatus();
    }

    void disactiveSelf()
    {
        statusManage.DisactiveStatus(StatusType);
    }

    void applyNextStatus()
    {
        if (NextStatusType != StatusType.None)
        {
            statusManage.OnApplyStatus(NextStatusType);
        }
    }
}
