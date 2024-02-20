using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class Door : MonoBehaviour
{
    /*[SerializeField] private string prompt;
    public string InteractionPrompt => prompt;*/
    public int doorValue;
    public int damage = 1;
    public GameObject character;
    public Transform respawnPoint;

    public CharacterHealth characterHealth;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            characterHealth.TakeDamage(damage);
            character.transform.position = respawnPoint.position;
        }
    }
}