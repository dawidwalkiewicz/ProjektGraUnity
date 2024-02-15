using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimableVest : MonoBehaviour
{
    public GameObject claimObjectText;
    public GameObject vestOnCharacter;
    public WardrobeDoor wardrobeDoor;

    private void Start()
    {
        vestOnCharacter.SetActive(false);
        claimObjectText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            claimObjectText.SetActive(true);
            if (Input.GetKey(KeyCode.Mouse0))
            {
                this.gameObject.SetActive(false);
                vestOnCharacter.SetActive(true);
                claimObjectText.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        claimObjectText.SetActive(false);
    }
}