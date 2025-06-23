using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour
{
    [SerializeField] MainBodyController mainSwapModule;

    [SerializeField] List<Body> swapBodyList = new List<Body>();

    Body[] setNewBody = new Body[3];


    [ContextMenu("Swap Menu")]
    void SwapBody()
    {

        for (int i = 0; i < setNewBody.Length; i++)
        {
            if (i >= swapBodyList.Count)
            {
                setNewBody[i] = null;
                continue;
            }
            setNewBody[i] = swapBodyList[i];
        }


        mainSwapModule.SetNewBody(setNewBody);

    }

}
