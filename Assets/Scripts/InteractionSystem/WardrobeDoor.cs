using UnityEngine;

public class WardrobeDoor : MonoBehaviour
{
    public AnimationCurve openSpeedCurve = new(new Keyframe[] { new(0, 1, 0, 0), new(0.8f, 1, 0, 0), new(1, 0, 0, 0) });
    public float openSpeedMultiplier = 2.0f;
    public float doorOpenAngle = 90.0f;

    bool doorOpen = false;
    bool enter = false;

    float defaultRotationAngle;
    float currentRotationAngle;
    float openTime = 0;

    void Start()
    {
        defaultRotationAngle = transform.localEulerAngles.y;
        currentRotationAngle = transform.localEulerAngles.y;

        GetComponent<Collider>().isTrigger = true;
    }

    // Main function
    void Update()
    {
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeedMultiplier * openSpeedCurve.Evaluate(openTime);
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle,
            defaultRotationAngle + (doorOpen ? doorOpenAngle : 0), openTime), transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.F) && enter)
        {
            doorOpen = !doorOpen;
            currentRotationAngle = transform.localEulerAngles.y;
            openTime = 0;
        }
    }

    void OnGUI()
    {
        if (enter)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 155, 30), "Press 'F' to " +
                (doorOpen ? "close" : "open") + " the door");
        }
    }
    //

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = false;
        }
    }
}