using UnityEngine;
using System.Collections;

public class CoreSwapper : MonoBehaviour
{
    [Header("SO settings")]
    [SerializeField] private CoreSwapSO configSO;   

    private Transform currentCore;     
    private bool isSwapping = false;    

    private RaycastHandler raycastHandler;

    private void Awake()
    {
        currentCore = transform;
        currentCore.tag = "Core";       
  
        raycastHandler = new RaycastHandler(Camera.main);  
    }

    private void Update()
    {
        // Trigger swap if the correct key is pressed
        if (!isSwapping && Input.GetKeyDown(configSO.swapKey))
        {
            StartCoroutine(SwapWithDelay());
        }
    }

    private IEnumerator SwapWithDelay()
    {
        isSwapping = true;
        yield return new WaitForSeconds(configSO.clickDelay);  

        // Perform the raycast
        Vector3 origin = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;

        RaycastHit? hit = raycastHandler.CastRay(origin, direction, configSO.maxDistance, configSO.coreLayerMask);
        if (hit.HasValue)
        {
            HandleHit(hit.Value);
        }
        else
        {
            HandleMiss();
        }

        isSwapping = false;
    }

    private void HandleHit(RaycastHit hit)
    {
        Debug.Log($"[Hit] Raycast hit: {hit.transform.name}");

      
        if (hit.transform.CompareTag("Core"))
        {
            DoSwap(hit.transform);
        }
        else if (hit.collider.CompareTag("Mirror"))
        {
            raycastHandler.HandleReflection(hit, configSO.maxDistance, configSO.coreLayerMask, DoSwap);
        }
        else
        {
            Debug.Log("[Hit] Hit is neither Core nor Mirror.");
        }
    }

    private void HandleMiss()
    {
        Debug.Log("[Miss] No hit detected.");
    }

    private void DoSwap(Transform hitCore)
    {
        Vector3 oldPos = currentCore.position;
        Vector3 newPos = hitCore.position + Vector3.up * 1.5f;
        Transform parentTransform = currentCore.parent;

        Debug.Log($"[Swap] oldPos = {oldPos}, newPos = {newPos}");
        GameObject oldCoreClone = Instantiate(configSO.corePrefab, oldPos, currentCore.rotation, parentTransform);
        oldCoreClone.tag = "Core";

        Destroy(hitCore.gameObject);   
        currentCore.position = newPos; 
    }
}
