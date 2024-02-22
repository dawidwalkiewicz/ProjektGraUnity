using UnityEngine;

public class AttachToCamera : MonoBehaviour
{
    public GameObject weapon;
    void Start()
    {
        weapon.transform.SetParent(Camera.main.transform);
        weapon.transform.SetLocalPositionAndRotation(new Vector3(0.4f, 0.1f, -0.8f), Quaternion.Euler(new Vector3(0f, -100f, -88f)));
    }
}