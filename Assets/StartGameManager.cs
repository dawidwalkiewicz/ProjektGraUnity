using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    public GameObject startGameButton;
    public ClaimableBelt belt;
    public ClaimableGloves gloves;
    public ClaimableHelm helm;
    public ClaimableMask mask;
    public ClaimableMeasureDevice measureDevice;
    public ClaimableShoes shoes;
    public ClaimableVest vest;
    public ClaimableWeapon weapon;
    void Start()
    {
        startGameButton.SetActive(false);
    }

    void Update()
    {
        if (belt.isClaimed && gloves.isClaimed && helm.isClaimed && mask.isClaimed && measureDevice.isClaimed && shoes.isClaimed
            && vest.isClaimed && weapon.isClaimed)
        {
            startGameButton.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}