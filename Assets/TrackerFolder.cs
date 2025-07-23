using UnityEngine;

public class TrackerFolder : MonoBehaviour
{
    Tracker[] TrackerList;
    void Start()
    {
        TrackerList = GetComponentsInChildren<Tracker>();
        int index = 0;
        foreach (Tracker tracker in TrackerList)
        {
            tracker.SetTrackerIndex(index);
            tracker.transform.rotation = Quaternion.Euler(0, this.transform.rotation.y + 180, 0);
            index++;
            if (index <= TrackerList.Length - 1)
            {
                tracker.futureTrack = TrackerList[index].transform;
            }
            else { tracker.futureTrack = TrackerList[0].transform; }
        }

    }
}
