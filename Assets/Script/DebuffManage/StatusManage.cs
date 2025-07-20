using System;
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
                foundStatus.AddStatusManage(this);
            }
        }
    }
    List<StatusType> currentActiveStatus = new List<StatusType>();
    public void OnApplyStatus(StatusType addedStatus)
    {
        Status getStatus = statusList.FirstOrDefault(i => i.StatusType == addedStatus);
        foreach (StatusType incompeteType in getStatus.InharmoniousStatus)
        {
            if (currentActiveStatus.Contains(incompeteType))
            {
                return;
            }
        }
        currentActiveStatus.Add(addedStatus);
        getStatus.ApplyEffect();
    }

    public void DisactiveStatus(StatusType unactiveStatus)
    {
        currentActiveStatus.Remove(unactiveStatus);
    }

}
