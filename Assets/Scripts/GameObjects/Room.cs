using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{
    public Wall wall1, wall2;
    public Ceiling ceiling;
    public Floor floor;
    public GameDataManager gdManager;
    public RegulationRing regulationRing;
    MeasureDevice measureDevice;

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
    }

    void Start()
    {
        if (regulationRing == null)
        {
            regulationRing = gdManager.regulationRing;
        }
        gdManager.statistics.NeutralizedRoomsCounter = 0;
    }

    void Update()
    {
        Door door = measureDevice.GetClosestDoor();
        if (door.IsNeutralized)
        {
            NeutralizeRoom();
        }
        if (gdManager.statistics.NeutralizedRoomsCounter == gdManager.rooms.Count)
        {
            FinishTheGame();
        }
    }

    public void NeutralizeRoom()
    {
        Room room = measureDevice.GetClosestRoom();
        if (room.regulationRing.numberOfRegulationKeyPressed == 0 && room.wall1.isWallHit && room.wall2.isWallHit
            && room.ceiling.isCeilingHit && room.floor.isFloorHit)
        {
            gdManager.statistics.NeutralizedRoomsCounter++;
        }
    }

    public void FinishTheGame()
    {
        gdManager.charHealth.timerScript.enabled = false;
        gdManager.charHealth.background.enabled = true;
        gdManager.charHealth.gameOverText.text = "Game completed in:"/* + gdManager.charHealth.timerScript.timerText.ToString()*/;
        gdManager.charHealth.gameOverText.enabled = true;
        gdManager.statistics.GameCompletionTime = gdManager.charHealth.timerScript.timerText.ToString();
        SceneManager.LoadScene("Phasis3");
    }
}