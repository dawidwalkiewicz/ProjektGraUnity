using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameSceneManager : MonoBehaviour
{
    public string phasisName;
    public void NewGameScene()
    {
        SceneManager.LoadScene(phasisName);
    }
}
