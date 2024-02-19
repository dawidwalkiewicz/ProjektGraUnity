using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDataManager : MonoBehaviour
{
    public FinalResultsScreen finalResultsScreen;
    public TimerScript timerScript;
    public Stats statistics;
    public RegulationRing regulationRing;
    public MeasureDevice measureDevice;

    public static GameDataManager instance;
    public Text lifeText;
    public Text gameOverText;
    public TMP_Text timerText;
    public Image background;
    public bool dead = false;
    public bool neutralizedDoors = false;
    public bool tooCloseToTheDoor = false;
    public Vector3 characterPosition;
    public RaycastHit rcHit;
    float distance;

    int livesLeft = 3;
    public List<Door> doors;
    public List<Wall> walls;
    public Ceiling ceiling;

    private void Awake()
    {
        instance = this;
        characterPosition = transform.position;
    }
    void Start()
    {
        Door door1 = gameObject.AddComponent<Door>();
        door1.doorValue = Random.Range(0, 9);
        Door door2 = gameObject.AddComponent<Door>();
        door2.doorValue = Random.Range(0, 9);
        Door door3 = gameObject.AddComponent<Door>();
        door3.doorValue = Random.Range(0, 9);
        Door door4 = gameObject.AddComponent<Door>();
        door4.doorValue = Random.Range(0, 9);
        Wall wall1 = gameObject.AddComponent<Wall>();
        Wall wall2 = gameObject.AddComponent<Wall>();
        ceiling = gameObject.AddComponent<Ceiling>();
        doors.Add(door1);
        doors.Add(door2);
        doors.Add(door3);
        doors.Add(door4);
        walls.Add(wall1);
        walls.Add(wall2);
        timerScript.enabled = true;
        lifeText.text = "x " + livesLeft.ToString();
        background.enabled = false;
        gameOverText.enabled = false;
        statistics.NeutralizedDoorsCounter = 0;
        statistics.UnneutralizedDoorsCounter = 4;
        statistics.NeutralizedRoomsCounter = 0;
        statistics.MissedWalls = 3;
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out rcHit))
        {
            distance = rcHit.distance;
        }
        NeutralizeDoor();
        return;
    }

    public void RemoveLife()
    {
        if (livesLeft > 0 && neutralizedDoors)
        {
            dead = true;
            livesLeft--;
            lifeText.text = "x " + livesLeft.ToString();
            transform.position = characterPosition;
            dead = false;
            tooCloseToTheDoor = false;
        }
        else
        {
            dead = true;
            timerScript.enabled = false;
            background.enabled = true;
            gameOverText.enabled = true;
            statistics.GameCompletionTime = timerScript.ToString();
            Time.timeScale = 0;
            SceneManager.LoadScene("Phasis3");
        }
    }

    public void NeutralizeDoor()
    {
        for (int i = 0; i <= doors.Count - 1; i++)
        {
            if (doors[i].doorValue > 0 && distance <= 0.3)
            {
                tooCloseToTheDoor = true;
                doors[i].GetComponent<MeshRenderer>().material.color = Color.red;
                RemoveLife();
            }
            else if (doors[i].doorValue > 0 && distance <= 3.5 && distance > 0.3)
            {
                regulationRing.RegulateDoor(doors);
            }
            else
            {
                neutralizedDoors = true;
                measureDevice.MeasureDoorValue();
                statistics.WasMeasurementSet = true;
                statistics.NeutralizedDoorsCounter++;
                statistics.UnneutralizedDoorsCounter--;
                //doors[i];
                ShootWalls();
            }
        }
    }

    public void ShootWalls()
    {
        for (int i = 0; i <= walls.Count - 1; i++)
        {
            walls[i].WasWallHit();
        }
        ceiling.WasCeilingHit();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door") && collision.gameObject.layer.Equals("whatIsDoor"))
        {
            RemoveLife();
        }
    }
}