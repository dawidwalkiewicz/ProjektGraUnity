using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimableShoes : MonoBehaviour
{
    public GameObject claimObjectText;
    public GameObject shoesOnCharacter;
    public WardrobeDoor wardrobeDoor;

    private void Start()
    {
        shoesOnCharacter.SetActive(false);
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
                shoesOnCharacter.SetActive(true);
                claimObjectText.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        claimObjectText.SetActive(false);
    }
}