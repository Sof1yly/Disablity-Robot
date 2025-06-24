using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class InteractionHandler : MonoBehaviour
{
    
    [SerializeField] UnityEvent interacted_event = new UnityEvent();
    [SerializeField] GameObject interactionIcon;
    [SerializeField] List<ScriptableObject> requireBodyPart = new List<ScriptableObject>();

    void Start()
    {
        
        if (interactionIcon != null)
        {
            interactionIcon.SetActive(false);
        }
    }

  
    private void OnTriggerStay(Collider other)
    {
     
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in the trigger!"); 

           
            if (interactionIcon != null)
            {
                interactionIcon.SetActive(true);
            }

          
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Space pressed! Interacting."); 
                interacted_event?.Invoke(); 
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger!");


            if (interactionIcon != null)
            {
                interactionIcon.SetActive(false);
            }
        }
    }
}