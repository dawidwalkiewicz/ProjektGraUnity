using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public int doorValue = Random.Range(0, 9);
    public GameDataManager gdManager;

    RaycastHit rcHit;
    float distance;
    public bool Interact(Interactor interactor)
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out rcHit))
        {
            distance = rcHit.distance;
            gdManager.Update();
        }
        return true;
        /*if (doorValue != 0 && distance <= 3.5 && distance > 0.3)
        {
            Debug.Log("Neutralize door");
            return true;
        }
        else if (doorValue != 0 && distance <= 0.3)
        {
            gdManager.Update();
            return true;
        }
        else
        {
            Debug.Log("Door neutralized");
            return true;
        }*/
    }
}