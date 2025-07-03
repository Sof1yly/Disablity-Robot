using UnityEngine;
using System.Collections;

public class RaycastCoreSwapper : MonoBehaviour
{
    //they still some bug I dont fucking know why 
    [Header("SO settings")]
    [SerializeField] private CoreSwapSO configSO; 

    [SerializeField] private Camera mainCamera;

    private Transform currentCore;                          
    private bool isSwapping = false;                        

    private void Awake()
    {
        currentCore = transform;
        currentCore.tag = "Core";

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
     
    private void Update()
    {
       Vector3 camPos = mainCamera.transform.position;
       Vector3 camDir = mainCamera.transform.forward;
       Debug.DrawLine(camPos, camPos + camDir * configSO.maxDistance, Color.blue);
        if (!isSwapping && Input.GetKeyDown(configSO.swapKey))
        {
            StartCoroutine(SwapWithDelay());
        }
      
    }

    private IEnumerator SwapWithDelay()
    {
        isSwapping = true;
        yield return new WaitForSeconds(configSO.clickDelay);

        Vector3 origin = mainCamera.transform.position + mainCamera.transform.forward *0.5f;
        Vector3 direction = mainCamera.transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, configSO.maxDistance, configSO.coreLayerMask,QueryTriggerInteraction.Ignore))
        {
            Debug.DrawRay(origin, direction * hit.distance, Color.red, 1f);
            Debug.Log($"[Swap] Raycast hit: {hit.transform.name}");

            if (hit.transform.CompareTag("Core") && hit.transform != currentCore)
            {
                DoSwap(hit.transform);
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
        if(hitCore == currentCore)
        {
            return;
        }

        Vector3 oldPos = currentCore.position;
        Vector3 newPos = hitCore.position+Vector3.up * 1.2f;

        Transform parentTransform = currentCore.parent;
                                                                                    
      
        GameObject clone = Instantiate(configSO.corePrefab, oldPos, Quaternion.identity, parentTransform);
        clone.tag = "Core"; // adjust your own tag name nigga

        Destroy(hitCore.gameObject);
    
        currentCore.position = newPos;

    }
}
