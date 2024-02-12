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
    public Text bulletsText;
    public Text gameOverText;
    public Image background;
    public bool dead = false;
    public bool neutralizedDoors = false;
    public bool tooCloseToTheDoor = false;
    public Vector3 characterPosition;
    public RaycastHit rcHit;
    float distance;

    int livesLeft = 3;
    readonly int magazineBulletsLeft = 30;
    readonly int totalBulletsLeft = 270;
    public Door doori;
    public Wall wall1, wall2;
    public Ceiling ceiling;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        lifeText.text = "x " + livesLeft.ToString();
        bulletsText.text = magazineBulletsLeft.ToString() + "/" + totalBulletsLeft.ToString();
        background.enabled = false;
        gameOverText.enabled = false;
        statistics.missedWalls = 3;
    }

    public void Update()
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
        if (!neutralizedDoors)
        {
            statistics.neutralizedDoorsCounter++;
            statistics.unneutralizedDoorsCounter--;
        }
        else if (livesLeft > 0 && neutralizedDoors)
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
            statistics.gameCompletionTime = timerScript.ToString();
            SceneManager.LoadScene("Phasis3");
        }
    }

    public void NeutralizeDoor()
    {
        for (int i = 1; i <= 4; i++)
        {
            if (doori.doorValue > 0 && distance <= 0.3)
            {
                doori.GetComponent<MeshRenderer>().material.color = Color.red;
                RemoveLife();
            }
            else if (doori.doorValue > 0 && distance <= 3.5 && distance > 0.3)
            {
                //regulationRing.Interact(doori);
            }
            else
            {
                measureDevice.Update();
                neutralizedDoors = true;
                statistics.neutralizedDoorsCounter++;
                statistics.unneutralizedDoorsCounter--;
                ShootWalls();
            }
        }
    }

    public void ShootWalls()
    {
        /*if (hitTarget.Wall == 1 && hitTarget.Ceiling == 1)
        {
            
        }*/
    }
}