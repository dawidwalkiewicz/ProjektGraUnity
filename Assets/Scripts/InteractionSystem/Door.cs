using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode neutralizeKey = KeyCode.N;

    public Stats statistics;
    public int doorValue;
    public int maxValue = 9;
    public int damage = 1;
    public bool IsNeutralized { get; set; }
    public GameObject character;
    public Transform respawnPoint;
    public Vector3 characterPosition;
    public RaycastHit rcHit;
    public float interactionDistance = 3.0f;

    public CharacterHealth characterHealth;
    public GameDataManager gdManager;
    public RegulationRing regulationRing;
    public bool tooCloseToTheDoor;

    void Awake()
    {
        characterPosition = new Vector3(-3f, 0f, 10f);
        statistics.NeutralizedDoorsCounter = 0;
        statistics.UnneutralizedDoorsCounter = 4;
        statistics.NeutralizedRoomsCounter = 0;
        statistics.MissedWalls = 3;
    }

    void Start()
    {
        tooCloseToTheDoor = false;
        IsNeutralized = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            characterHealth.TakeDamage(damage);
            character.transform.position = respawnPoint.position;
        }
    }

    public void ChangeDoorPosition(int index)
    {
        if (index >= 0 && index < gdManager.doors.Count)
        {
            gdManager.doors[index].transform.position += new Vector3(0f, 10f, 0f);
        }
    }

    public void NeutralizeDoor()
    {
        float distance = Vector3.Distance(character.transform.position, transform.position);
        for (int i = 0; i < gdManager.doors.Count; i++)
        {
            Door door = gdManager.doors[i].GetComponent<Door>();
            if (door != null && distance <= interactionDistance)
            {
                if (door.doorValue > 0)
                {
                    tooCloseToTheDoor = true;
                    gdManager.doors[i].GetComponent<MeshRenderer>().material.color = Color.red;
                    regulationRing.RegulateDoor(gdManager.doors);
                }
                else if (door.doorValue == 0 && distance <= interactionDistance && Input.GetKey(neutralizeKey))
                {
                    door.IsNeutralized = true;
                    statistics.WasMeasurementSet = true;
                    statistics.NeutralizedDoorsCounter++;
                    statistics.UnneutralizedDoorsCounter--;
                    ChangeDoorPosition(i);
                }
            }
            door.IsNeutralized = false;
        }
    }
}