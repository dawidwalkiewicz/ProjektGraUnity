using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalResultsScreen : MonoBehaviour
{
    public Text resultsText;
    public GameObject tryAgainButton;
    public Stats statistics;

    void Start()
    {
        statistics = GameDataManager.instance.statistics;
    }

    void OnEnable()
    {
        Setup();
    }
    
    void Update()
    {
        ResetTheGame();
    }

    public void Setup()
    {
        if (resultsText != null && statistics != null)
        {
            resultsText.text = "Time: " + statistics.GameCompletionTime + System.Environment.NewLine +
                "Neutralized doors: " + statistics.NeutralizedDoorsCounter + System.Environment.NewLine
                + "Unneutralized doors: " + statistics.UnneutralizedDoorsCounter +
                System.Environment.NewLine + "Neutralized rooms: " + statistics.NeutralizedRoomsCounter;
            if (statistics.WasMeasurementSet == false)
            {
                resultsText.text += System.Environment.NewLine + "Measurement was not set";
            }
            if (statistics.TooHighValue == true)
            {
                resultsText.text += System.Environment.NewLine + "Too high value set";
            }
            if (statistics.TooLowValue == true)
            {
                resultsText.text += System.Environment.NewLine + "Too low value set";
            }
            if (statistics.WrongRingSettings == true)
            {
                resultsText.text += System.Environment.NewLine + "Wrong ring settings";
            }
            if (statistics.MissedWalls > 0)
            {
                resultsText.text += System.Environment.NewLine + "Missed walls: " + statistics.MissedWalls;
            }
            else
            {
                resultsText.text += System.Environment.NewLine + "Missed walls: 0";
            }
            if (statistics.WallsHitMoreThanOnce > 0)
            {
                resultsText.text += System.Environment.NewLine + "Walls hit more than once: " + statistics.MissedWalls;
            }
            else
            {
                resultsText.text += System.Environment.NewLine + "Walls hit more than once: 0";
            }
        }
    }

    public void ResetTheGame()
    {
        statistics.GetComponent<Stats>().ResetStats();
        print("The button is working.");
        SceneManager.LoadScene("Phasis2");
    }
}