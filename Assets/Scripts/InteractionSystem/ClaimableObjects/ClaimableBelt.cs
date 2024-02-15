using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClaimableBelt : MonoBehaviour
{
    public GameObject claimObjectText;
    public GameObject beltOnCharacter;
    public WardrobeDoor wardrobeDoor;

    private void Start()
    {
        beltOnCharacter.SetActive(false);
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
                beltOnCharacter.SetActive(true);
                claimObjectText.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        claimObjectText.SetActive(false);
    }
}