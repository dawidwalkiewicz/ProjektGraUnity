using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode neutralizeKey = KeyCode.N;

    public Stats statistics;
    [SerializeField]
    public int doorValue;

    public int minValue = 0;
    public int maxValue = 9;
    public int damage = 1;
    public bool IsNeutralized { get; set; }
    public GameObject character;
    public Transform respawnPoint;
    public Vector3 characterPosition;
    public RaycastHit rcHit;
    public float interactionDistance = 1.0f;

    public CharacterHealth characterHealth;
    private GameDataManager gdManager;
    List<Door> doors;
    public RegulationRing regulationRing;
    public bool tooCloseToTheDoor;

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
        characterPosition = new Vector3(-3f, 0f, 10f);
        statistics.NeutralizedDoorsCounter = 0;
        statistics.UnneutralizedDoorsCounter = 4;
        statistics.NeutralizedRoomsCounter = 0;
    }

    void Start()
    {
        doors = gdManager.doors;
        tooCloseToTheDoor = false;
        IsNeutralized = false;
    }

    void Update()
    {
        NeutralizeDoor();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            characterHealth.TakeDamage(damage);
            character.transform.position = respawnPoint.position;
        }
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
        for (int i = 0; i < doors.Count; i++)
        {
            Door door = doors[i];
            if (door != null)
            {
                if (door.doorValue > minValue && distance <= interactionDistance)
                {
                    tooCloseToTheDoor = true;
                    doors[i].GetComponent<MeshRenderer>().material.color = Color.red;
                    regulationRing.RegulateDoor(door);
                }
                else if (door.doorValue == minValue && distance <= interactionDistance && Input.GetKey(neutralizeKey))
                {
                    door.IsNeutralized = true;
                    statistics.WasMeasurementSet = true;
                    statistics.NeutralizedDoorsCounter++;
                    statistics.UnneutralizedDoorsCounter--;
                    ChangeDoorPosition(i);
                    statistics.NeutralizedRoomsCounter++;
                }
            }
        }
    }
}