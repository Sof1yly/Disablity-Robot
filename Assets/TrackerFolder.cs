using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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
            if (index <= TrackerList.Length - 1)
            {
                tracker.futureTrack = TrackerList[index].transform;
            }
            else { tracker.futureTrack = TrackerList[0].transform; }
        }

    }
}
