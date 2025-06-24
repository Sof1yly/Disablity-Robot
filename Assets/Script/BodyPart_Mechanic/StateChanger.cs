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

                if (swapBodyList[i]?.PartPrefabs == null)
                {
                    swapBodyList[i].PartPrefabs.SetActive(false);
                }
                continue;
            }
            else
            {
                setNewBody[i] = swapBodyList[i];
            }
            
        }

        mainSwapModule.SetNewBody(setNewBody);
    }

    public bool HasLeg()
    {
        foreach (Body body in swapBodyList)
        {
            if (body != null && body.PartName == "Leg")
            {
                return true;
            }
        }
        return false; 
    }
}
