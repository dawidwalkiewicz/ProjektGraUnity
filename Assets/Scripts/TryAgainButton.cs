using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour
{
    public void OnTryAgainButtonClicked()
    {
        SceneManager.LoadScene("Phasis2");
    }
}