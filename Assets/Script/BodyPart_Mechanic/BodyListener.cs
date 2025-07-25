using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyListener : MonoBehaviour
{
    [SerializeField] Body bodyType;

    [SerializeField] List<Activeable> activeList = new System.Collections.Generic.List<Activeable>();

    Action onActive;
    Action onInActive;

    private void Start()
    {
        ObjectFinder.Instance.GetStoredComponent<MainBodyController>().OnBodyChanged += updateBodyState;
        setUpEvent();
    }


    [ContextMenu("Add Active Object")]
    void addActiveObject()
    {
        foreach (Transform i in this.transform)
        {
            Activeable newActivAble = i.GetComponent<Activeable>();
            if (newActivAble != null && activeList.Contains(newActivAble) == false)
            {
                activeList.Add(newActivAble);
            }
        }
    }

    void setUpEvent()
    {
        foreach (Activeable activeAble in activeList)
        {
            activeAble.SetUpEvent(ref onActive, ref onInActive);
        }
    }

    void updateBodyState(Body[] selectedBody)
    {
        if (selectedBody.Contains(bodyType))
        {
            onActive?.Invoke();
        }
        else
        {
            onInActive?.Invoke();
        }
    }
}
