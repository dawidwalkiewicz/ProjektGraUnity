using UnityEngine;
using UnityEngine.UI;

public class ClaimableWeapon : MonoBehaviour
{
    public GameObject claimObjectText;
    public GameObject weaponOnCharacter;
    public WardrobeDoor wardrobeDoor;
    public bool isClaimed;

    private void Start()
    {
        weaponOnCharacter.SetActive(false);
        claimObjectText.SetActive(false);
        isClaimed = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            claimObjectText.SetActive(true);
            if (Input.GetKey(KeyCode.Mouse0))
            {
                this.gameObject.SetActive(false);
                weaponOnCharacter.SetActive(true);
                claimObjectText.SetActive(false);
                isClaimed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        claimObjectText.SetActive(false);
    }
}