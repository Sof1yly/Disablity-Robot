using UnityEngine;
using System.Collections;

public class RaycastCoreSwapper : MonoBehaviour
{
    //they still some bug I dont fucking know why 
    [Header("SO settings")]
    [SerializeField] private CoreSwapSO configSO;

    private Transform currentCore;                          
    private bool isSwapping = false;                        

    private void Awake()
    {
        currentCore = transform;
        currentCore.tag = "Core";                           
    }
     
    private void Update()
    {
        Debug.DrawLine(currentCore.position, currentCore.position + currentCore.forward * configSO.maxDistance, Color.green);
        if (!isSwapping && Input.GetKeyDown(configSO.swapKey))
        {
            StartCoroutine(SwapWithDelay());
        }
      
    }

    private IEnumerator SwapWithDelay()
    {
        isSwapping = true;
        Debug.Log(" waiting ");
        yield return new WaitForSeconds(configSO.clickDelay);

        Vector3 origin = currentCore.position + Vector3.down * 0.22f;
        Vector3 direction = currentCore.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, configSO.maxDistance, configSO.coreLayerMask))
        {
            Debug.DrawRay(origin, direction * hit.distance, Color.red, 1f);
            Debug.Log($"[Swap] Raycast hit: {hit.transform.name}");

            if (hit.transform.CompareTag("Core") && hit.transform != currentCore)
            {
                DoSwap(hit.transform);
            }
            else
            {
                Debug.Log("[Swap] Hit core is invalid or same as current");
            }
        }
        else
        {
            Debug.DrawRay(origin, direction * configSO.maxDistance, Color.yellow, 1f);

        }

        isSwapping = false;
    }

    private void DoSwap(Transform hitCore)
    {

        Vector3 oldPos = currentCore.position;
        Vector3 newPos = hitCore.position+Vector3.up*1.5f;
        Transform parentTransform = currentCore.parent;
                                                                                    
        Debug.Log($"[Swap] oldPos = {oldPos}, newPos = {newPos}");
        GameObject oldCoreClone = Instantiate(configSO.corePrefab, oldPos, currentCore.rotation, parentTransform);
        oldCoreClone.tag = "Core";

        Destroy(hitCore.gameObject);
    
        currentCore.position = newPos;
    }
}
