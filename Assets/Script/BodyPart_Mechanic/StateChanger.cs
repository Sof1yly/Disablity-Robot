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
        for (int j = 0; j < swapBodyList.Count; j++)
        {
            Body b = swapBodyList[j];
            if (b != null && b.PartPrefabs != null)
            {
                if (j == 0)
                {
                    b.PartPrefabs.SetActive(true);
                }
                else
                {
                    b.PartPrefabs.SetActive(false);
                }

            }
        }

        for (int i = 0; i < setNewBody.Length; i++)
        {
            if (i == 0 && swapBodyList.Count > 0 && swapBodyList[0] != null)
                setNewBody[i] = swapBodyList[0];
            else
                setNewBody[i] = null;
        }


        mainSwapModule.SetNewBody(setNewBody);
    }

}