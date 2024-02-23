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
    public TMP_Text timerText;
    public Image background;
    public TimerScript timerScript;
    public Stats statistics;
    public List<Door> doors;

    void Awake()
    {
        if (statistics == null)
        {
            statistics = GameObject.Find("Stats").GetComponent<Stats>();
        }
        statistics.GameCompletionTime = "0:00,000";
    }    

    void Start()
    {
        health = maxHealth;
        dead = false;
        for (int i = 0; i < doors.Count; i++)
        {
            tooCloseToTheDoor = doors[i].tooCloseToTheDoor;
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
            Destroy(gameObject);
            dead = true;
            timerScript.enabled = false;
            background.enabled = true;
            gameOverText.enabled = true;
            statistics.GameCompletionTime = timerScript.ToString();
            //Time.timeScale = 0;
            SceneManager.LoadScene("Phasis3");
        }
        else
        {
            dead = true;
            lifeText.text = "x " + health.ToString();
            dead = false;
            tooCloseToTheDoor = false;
        }
    }
}