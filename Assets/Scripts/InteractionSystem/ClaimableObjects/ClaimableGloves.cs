using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimableGloves : MonoBehaviour
{
    public GameObject claimObjectText;
    public GameObject glovesOnCharacter;
    public WardrobeDoor wardrobeDoor;
    public bool isClaimed;

    private void Start()
    {
        glovesOnCharacter.SetActive(false);
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
                glovesOnCharacter.SetActive(true);
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