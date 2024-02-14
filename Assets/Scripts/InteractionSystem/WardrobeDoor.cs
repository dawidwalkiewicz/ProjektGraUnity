using UnityEngine;

public class WardrobeDoor : MonoBehaviour
{
    Animator _wardrobeDoorAnim;
    private void OnTriggerEnter(Collider other)
    {
        _wardrobeDoorAnim.SetBool("isOpening", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _wardrobeDoorAnim.SetBool("isOpening", false);
    }

    void Start()
    {
        _wardrobeDoorAnim = this.transform.parent.GetComponent<Animator>();
    }
}