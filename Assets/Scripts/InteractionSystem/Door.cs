using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public int doorValue;
    public int damage = 1;

    public CharacterHealth characterHealth;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            characterHealth.TakeDamage(damage);
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}