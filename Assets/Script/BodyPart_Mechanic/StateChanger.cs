using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour
{
    [SerializeField] MainBodyController mainSwapModule;
    [SerializeField] List<Body> swapBodyList = new List<Body>();

    Body[] setNewBody = new Body[3];

    [ContextMenu("Swap Body Test")]
    void SwapBody()
    {
        for (int i = 0; i < setNewBody.Length; i++)
        {
            if (i <= swapBodyList.Count - 1)
            {
                setNewBody[i] = swapBodyList[i];
                Debug.Log($"Played Count {i} , Set Obj");

            }
            else
            {
                setNewBody[i] = null;
                Debug.Log($"Played Count {i} , Set To Null");

            }
        }


        mainSwapModule.SetNewBody(setNewBody);
    }


    public void AssignNewBody(List<Body> newBodyList)
    {
        swapBodyList = new List<Body>(newBodyList);
        Debug.Log($"swapBody Count {swapBodyList.Count}");
        SwapBody();
    }

}