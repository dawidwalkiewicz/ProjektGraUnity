using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool dead;
    public bool tooCloseToTheDoor;
    public Text lifeText;
    public Text gameOverText;
    public Image background;
    public TimerScript timerScript;
    public GameDataManager gdManager;

    void Awake()
    {
        if (gdManager == null)
        {
            gdManager = GameObject.Find("GameDataManager").GetComponent<GameDataManager>();
        }
        gdManager.statistics.GameCompletionTime = "0:00,000";
    }    

    void Start()
    {
        health = maxHealth;
        dead = false;
        for (int i = 0; i < gdManager.doors.Count; i++)
        {
            tooCloseToTheDoor = gdManager.doors[i].tooCloseToTheDoor;
        }
        lifeText.text = "x " + health.ToString();
        background.enabled = false;
        gameOverText.enabled = false; 
        timerScript.enabled = true;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            dead = true;
            timerScript.enabled = false;
            background.enabled = true;
            gameOverText.enabled = true;
            gdManager.statistics.GameCompletionTime = timerScript.timerText.ToString();
            Destroy(gameObject);
            SceneManager.LoadScene("Phasis3");
        }
        else
        {
            lifeText.text = "x " + health.ToString();
            tooCloseToTheDoor = false;
        }
    }
}