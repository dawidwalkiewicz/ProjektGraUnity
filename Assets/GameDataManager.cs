using System.Collections.Generic;
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
    }
    void Start()
    {
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
            characterPosition = new Vector3(0f, 0f, 0f);
            dead = false;
        }
        else
        {
            dead = true;
            timerScript.enabled = false;
            background.enabled = true;
            gameOverText.enabled = true;
            statistics.GameCompletionTime = timerScript.ToString();
            SceneManager.LoadScene("Phasis3");
        }
    }

    public void NeutralizeDoor()
    {
        for (int i = 1; i <= doors.Count; i++)
        {
            if (doors[i].doorValue > 0 && distance <= 0.3)
            {
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
                ShootWalls();
            }
        }
    }

    public void ShootWalls()
    {
        /*if (hitTarget.Wall == 1 && hitTarget.Ceiling == 1)
        {
            doors[i].OnTriggerEnter(doors[i]);
        }*/
    }
}