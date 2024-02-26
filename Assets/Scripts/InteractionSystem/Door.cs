using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode neutralizeKey = KeyCode.N;

    public int doorValue;
    public int minValue = 0;
    public int maxValue = 9;
    public int damage = 1;
    public bool IsNeutralized { get; set; }
    public GameObject character;
    public Transform respawnPoint;
    public Vector3 characterPosition;
    public RaycastHit rcHit;

    public GameDataManager gdManager;
    public RegulationRing regulationRing;
    MeasureDevice measureDevice;
    public bool tooCloseToTheDoor = false;
    private bool isFrozen = false;
    private bool hasNeutralizeKeyBeenPressed = false;
    private Color defaultColor;

    void Awake()
    {
        if (gdManager == null)
        {
            gdManager = GameObject.Find("GameDataManager").GetComponent<GameDataManager>();
        }
        if (measureDevice == null)
        {
            measureDevice = GameObject.Find("MeasureDevice").GetComponent<MeasureDevice>();
        }
        characterPosition = new Vector3(-20f, 0f, 10f);
    }

    void Start()
    {
        tooCloseToTheDoor = false;
        IsNeutralized = false;
        defaultColor = GetComponent<MeshRenderer>().material.color;
        gdManager.statistics.NeutralizedDoorsCounter = 0;
        gdManager.statistics.UnneutralizedDoorsCounter = 4;
        gdManager.statistics.WasMeasurementSet = false;
    }

    void Update()
    {
        NeutralizeDoor();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Door door = measureDevice.GetClosestDoor();
            tooCloseToTheDoor = true;
            door.GetComponent<MeshRenderer>().material.color = Color.red;
            gdManager.charHealth.TakeDamage(damage);
            if (!isFrozen)
            {
                StartCoroutine(FreezeCharacter());
                character.transform.position = respawnPoint.position;
                door.GetComponent<MeshRenderer>().material.color = defaultColor;
                tooCloseToTheDoor = false;
            }
        }
    }

    IEnumerator FreezeCharacter()
    {
        isFrozen = true;
        character.GetComponent<CharacterMovement>().canMove = false;
        yield return new WaitForSeconds(2f);
        character.GetComponent<CharacterMovement>().canMove = true;
        isFrozen = false;
    }

    private void ChangeDoorPosition(int index)
    {
        if (index >= 0 && index < gdManager.doors.Count)
        {
            gdManager.doors[index].transform.position += new Vector3(0f, 10f, 0f);
        }
    }

    public void NeutralizeDoor()
    {
        float distance = Vector3.Distance(character.transform.position, transform.position);
        float interactionDistance = 10.0f;
        Door door = measureDevice.GetClosestDoor();
        if (door != null)
        {
            if (!door.IsNeutralized)
            {
                if (door.doorValue == minValue && distance <= interactionDistance && Input.GetKey(neutralizeKey) && !hasNeutralizeKeyBeenPressed)
                {
                    door.IsNeutralized = true;
                    gdManager.statistics.WasMeasurementSet = true;
                    gdManager.statistics.NeutralizedDoorsCounter++;
                    gdManager.statistics.UnneutralizedDoorsCounter--;
                    int index = gdManager.doors.IndexOf(door);
                    ChangeDoorPosition(index);
                    hasNeutralizeKeyBeenPressed = true;
                }
            }
            else if (door.IsNeutralized && distance <= interactionDistance && Input.GetKeyDown(regulationRing.regulatePlusKey))
            {
                regulationRing.RegulateDoor(door);
                if (regulationRing.numberOfRegulationKeyPressed == 0)
                {
                    hasNeutralizeKeyBeenPressed = false;
                }
            }
        }
    }
}