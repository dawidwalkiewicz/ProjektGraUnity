using UnityEngine;
using UnityEngine.UI;

public class FinalResultsScreen : MonoBehaviour
{
    public Text resultsText;
    public GameObject tryAgainButton;
    public Stats statistics;

    void Start()
    {
        tryAgainButton.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void OnEnable()
    {
        Setup();
    }

    public void Setup()
    {
        if (resultsText != null && statistics != null)
        {
            resultsText.text = "Time: " + GameDataManager.instance.statistics.GameCompletionTime + System.Environment.NewLine +
                "Neutralized doors: " + GameDataManager.instance.statistics.NeutralizedDoorsCounter + System.Environment.NewLine
                + "Unneutralized doors: " + GameDataManager.instance.statistics.UnneutralizedDoorsCounter +
                System.Environment.NewLine + "Neutralized rooms: " + GameDataManager.instance.statistics.NeutralizedRoomsCounter;
            if (GameDataManager.instance.statistics.WasMeasurementSet == false)
            {
                resultsText.text += System.Environment.NewLine + "Measurement was not set";
            }
            if (GameDataManager.instance.statistics.TooHighValue == true)
            {
                resultsText.text += System.Environment.NewLine + "Too high value set";
            }
            if (GameDataManager.instance.statistics.TooLowValue == true)
            {
                resultsText.text += System.Environment.NewLine + "Too low value set";
            }
            if (GameDataManager.instance.statistics.WrongRingSettings == true)
            {
                resultsText.text += System.Environment.NewLine + "Wrong ring settings";
            }
            if (GameDataManager.instance.statistics.MissedWalls > 0)
            {
                resultsText.text += System.Environment.NewLine + "Missed walls: " + statistics.MissedWalls;
            }
            else
            {
                resultsText.text += System.Environment.NewLine + "Missed walls: 0";
            }
        }
    }
}