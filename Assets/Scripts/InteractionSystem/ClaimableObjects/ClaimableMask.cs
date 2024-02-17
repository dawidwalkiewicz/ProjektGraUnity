using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimableMask : MonoBehaviour
{
    public GameObject claimObjectText;
    public GameObject maskOnCharacter;
    public WardrobeMasksDoor wardrobeDoor;
    public bool isClaimed;

    private void Start()
    {
        maskOnCharacter.SetActive(false);
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
                maskOnCharacter.SetActive(true);
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