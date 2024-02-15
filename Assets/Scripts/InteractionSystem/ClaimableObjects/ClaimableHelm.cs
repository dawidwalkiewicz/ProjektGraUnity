using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimableHelm : MonoBehaviour
{
    public GameObject claimObjectText;
    public GameObject helmOnCharacter;
    public WardrobeDoor wardrobeDoor;

    private void Start()
    {
        helmOnCharacter.SetActive(false);
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
                helmOnCharacter.SetActive(true);
                claimObjectText.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        claimObjectText.SetActive(false);
    }
}