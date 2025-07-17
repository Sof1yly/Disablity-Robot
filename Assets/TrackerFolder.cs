using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TrackerFolder : MonoBehaviour
{
    Tracker[] TrackerList;
    void Start()
    {
        TrackerList = GetComponentsInChildren<Tracker> ();
        int index = 0;
        foreach (Tracker tracker in TrackerList)
        {
            tracker.SetTrackerIndex(index);
            index++;
        }

    }
}
