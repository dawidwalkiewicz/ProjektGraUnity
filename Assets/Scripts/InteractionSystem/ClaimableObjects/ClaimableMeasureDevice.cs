using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimableMeasureDevice : MonoBehaviour
{
    public GameObject claimObjectText;
    public GameObject measureDeviceOnCharacter;
    public WardrobeDoor wardrobeDoor;

    private void Start()
    {
        measureDeviceOnCharacter.SetActive(false);
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
                measureDeviceOnCharacter.SetActive(true);
                claimObjectText.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        claimObjectText.SetActive(false);
    }
}