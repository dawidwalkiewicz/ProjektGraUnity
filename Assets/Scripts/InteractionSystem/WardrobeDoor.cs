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

    void Update()
    {
        if (_wardrobeDoorAnim != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _wardrobeDoorAnim.Play("OpeningWardrobeClothesDoorAnimation");
        }
    }

    public bool AnimatorIsPlaying()
    {
        return _wardrobeDoorAnim.GetCurrentAnimatorStateInfo(0).length >
               _wardrobeDoorAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}