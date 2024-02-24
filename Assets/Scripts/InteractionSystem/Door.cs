using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode neutralizeKey = KeyCode.N;

    public Stats statistics;
    public int doorValue;

    public int minValue = 0;
    public int maxValue = 9;
    public int damage = 1;
    public bool IsNeutralized { get; set; }
    public GameObject character;
    public Transform respawnPoint;
    public Vector3 characterPosition;
    public RaycastHit rcHit;

    public CharacterHealth characterHealth;
    private GameDataManager gdManager;
    List<Door> doors;
    public RegulationRing regulationRing;
    MeasureDevice measureDevice;
    public bool tooCloseToTheDoor = false;
    private bool isFrozen = false;
    private bool hasNeutralizeKeyBeenPressed = false;
    private Color defaultColor;

    void Awake()
    {
        if (statistics == null)
        {
            statistics = GameObject.Find("Stats").GetComponent<Stats>();
        }
        if (gdManager == null)
        {
            gdManager = GameObject.Find("GameDataManager").GetComponent<GameDataManager>();
        }
        if (measureDevice == null)
        {
            measureDevice = GameObject.Find("MeasureDevice").GetComponent<MeasureDevice>();
        }
        characterPosition = new Vector3(-20f, 0f, 10f);
        statistics.NeutralizedDoorsCounter = 0;
        statistics.UnneutralizedDoorsCounter = 4;
        statistics.NeutralizedRoomsCounter = 0;
        statistics.WasMeasurementSet = false;
    }

    void Start()
    {
        doors = gdManager.doors;
        tooCloseToTheDoor = false;
        IsNeutralized = false;
        defaultColor = GetComponent<MeshRenderer>().material.color;
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
            characterHealth.TakeDamage(damage);
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
        yield return new WaitForSeconds(5f);
        character.GetComponent<CharacterMovement>().canMove = true;
        isFrozen = false;
    }

    private void ChangeDoorPosition(int index)
    {
        if (index >= 0 && index < doors.Count)
        {
            doors[index].transform.position += new Vector3(0f, 10f, 0f);
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
                if (door.doorValue == minValue && distance <= interactionDistance && Input.GetKeyDown(neutralizeKey) && !hasNeutralizeKeyBeenPressed)
                {
                    door.IsNeutralized = true;
                    statistics.WasMeasurementSet = true;
                    statistics.NeutralizedDoorsCounter++;
                    statistics.UnneutralizedDoorsCounter--;
                    int index = doors.IndexOf(door);
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