using UnityEngine;

public class ClaimableObject : MonoBehaviour, IInteractableInterface
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit rcHit;
    readonly Interactor claimableObjectInteractor;
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Claim");
        return true;
    }

    private void Update()
    {
        if (InteractionPrompt != null && Physics.Raycast(ray, out rcHit))
        {
            Interact(claimableObjectInteractor);
        }
    }
}
