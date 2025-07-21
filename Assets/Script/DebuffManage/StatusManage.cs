using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum StatusType
{ None, Stun, Iframe, Invincible, VisualObscured } // add new Status Here


public class StatusManage : MonoBehaviour
{
    Status currentStatus;
    public event Action OnActiveStatus;
    public event Action UnActiveStatus;

    List<Status> statusList = new List<Status>();
    List<StatusType> currentActiveStatus = new List<StatusType>();
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
    [SerializeField] StatusType DebugStatus;
    [ContextMenu("Apply Status")]
    void DebugApply()
    {
        OnApplyStatus(DebugStatus);
    }

    public void OnApplyStatus(StatusType addedStatus)
    {
        Status getStatus = statusList.FirstOrDefault(i => i.StatusType == addedStatus);
        if (getStatus == null)
        {
            Debug.LogWarning($"Status does not exit , can't apply :(");
            return;
        }
        foreach (StatusType incompeteType in getStatus.InharmoniousStatus)
        {
            if (currentActiveStatus.Contains(incompeteType))
            {
                return;
            }
        }
        if (currentActiveStatus.Contains(addedStatus)) return;


        currentActiveStatus.Add(addedStatus);
        getStatus.ApplyEffect();
    }

    public void DisactiveStatus(StatusType unactiveStatus)
    {
        currentActiveStatus.Remove(unactiveStatus);
    }

}
