using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalResultsScreen : MonoBehaviour
{
    public Image background;
    public Text resultsText;
    public Button tryAgainButton;
    public Stats statistics;
    public void Setup()
    {
        background.enabled = true;
        resultsText.text = "Time: " + statistics.gameCompletionTime + "/nNeutralized doors: " + statistics.neutralizedDoorsCounter +
            "/nUnneutralized doors: " + statistics.unneutralizedDoorsCounter +
            "/nNeutralized rooms: " + statistics.neutralizedRoomsCounter;
        if (statistics.wasMeasurementSet == false)
        {
            resultsText.text += "/nMeasurement was not set";
        }
        if (statistics.tooHighValue == true)
        {
            resultsText.text += "/nToo high value set";
        }
        if (statistics.tooLowValue == true)
        {
            resultsText.text += "/nToo low value set";
        }
        if (statistics.wrongRingSettings == true)
        {
            resultsText.text += "/nWrong ring settings";
        }
        if (statistics.missedWalls > 0)
        {
            resultsText.text += "/nMissed walls: " + statistics.missedWalls;
        }
        else
        {
            resultsText.text += "/nMissed walls: 0";
        }
    }

    public void NewGame()
    {
        if (tryAgainButton.onClick != null)
        {
            SceneManager.LoadScene("Phasis2");
        }
    }
}