using System.Collections.Generic;
using UnityEngine;

public class SetActiveTrueFalseController : Activeable
{
    [SerializeField] List<GameObject> ActiveList = new List<GameObject>();
    [SerializeField] List<GameObject> UnActivelist = new List<GameObject>();

    protected override void Active()
    {
        foreach (GameObject obj in ActiveList)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in UnActivelist)
        {
            obj.SetActive(false);
        }
    }

    protected override void InActive()
    {
        foreach (GameObject obj in ActiveList)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in UnActivelist)
        {
            obj.SetActive(true);
        }
    }
}
