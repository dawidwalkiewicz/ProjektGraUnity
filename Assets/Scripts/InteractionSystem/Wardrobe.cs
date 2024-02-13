using System;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour, IInteractableInterface
{
    public List<ClaimableObject> claimableObjects;
    public string InteractionPrompt { get; }
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit rcHit;
    public bool Interact(Interactor interactor)
    {
        for (int i = 1; i <= claimableObjects.Count; i++)
        {
            if (InteractionPrompt != null && Physics.Raycast(ray, out rcHit))
            {
                claimableObjects[i].transform.position = new Vector3(1f + i, 0f, 1f + i);
            }
        }
        return true;
    }
}