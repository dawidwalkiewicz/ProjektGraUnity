using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private List<IInteractableInterface> interactablesInRange = new List<IInteractableInterface>();
    void Update()
    {
        /*if (Input.GetButtonDown("Open") && interactablesInRange.Count > 0)
        {
            var interactable = interactablesInRange[0];
            interactable.Interact();
            if (!interactable.CanInteract())
            {
                interactablesInRange.Remove(interactable);
            }
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractableInterface>();
        /*if (interactable != null && interactable.CanInteract())
        {
            interactablesInRange.Add(interactable);
        }*/
    }
    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IInteractableInterface>();
        if (interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Remove(interactable);
        }
    }
}
