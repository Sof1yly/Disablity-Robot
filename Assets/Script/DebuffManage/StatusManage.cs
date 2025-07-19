using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum StatusType
{ None } // add new Status Here


public class StatusManage : MonoBehaviour
{
    Status currentStatus;
    public event Action OnActiveStatus;
    public event Action UnActiveStatus;

    List<Status> statusList = new List<Status> { null };
    private void Awake()
    {
        setUpList();
    }
    void setUpList()
    {
        foreach (Transform child in this.transform)
        {
            if (child.TryGetComponent<Status>(out Status foundStatus))
            {
                statusList.Add(foundStatus);
            }
        }
    }

    public void OnApplyStatus(StatusType addedStatus)
    {
        if (currentStatus != null) return;
        currentStatus = statusList.FirstOrDefault(i => i.StatusType == addedStatus);
        StartCoroutine(statusTimer(currentStatus));
    }

    IEnumerator statusTimer(Status status)
    {
        OnActiveStatus?.Invoke();
        status.OnActive();
        yield return new WaitForSeconds(status.DebuffTime);
        UnActiveStatus?.Invoke();
        status.OffActive();
        currentStatus = null;
        if (currentStatus.NextStatusType != StatusType.None)
        {
            OnApplyStatus(currentStatus.NextStatusType);
        }

    }
}
