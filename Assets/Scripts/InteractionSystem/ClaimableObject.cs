using UnityEngine;

public class ClaimableObject : MonoBehaviour, IInteractableInterface
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Claim");
        return true;
    }
}
