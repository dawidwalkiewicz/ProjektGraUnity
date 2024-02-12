using System;
using UnityEngine;

public class Wardrobe : MonoBehaviour, IInteractableInterface
{
    public ClaimableObject claimableObject;
    public string InteractionPrompt { get; }
    public bool Interact(Interactor interactor)
    {
        //throw new NotImplementedException();
        return true;
    }
}