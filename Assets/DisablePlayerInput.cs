using DavidJalbert;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayerInput : MonoBehaviour
{
    [SerializeField] List<TinyCarStandardInput> tinyCarInput = new List<TinyCarStandardInput>();

    public void disableAll()
    {
        foreach (var input in tinyCarInput)
        {
            input.enabled = false;
        }
    }

    public void enableAll()
    {
        foreach (var input in tinyCarInput)
        {
            input.enabled = true;
        }
    }
}
