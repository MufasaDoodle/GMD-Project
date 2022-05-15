using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    

    public void OnPlayPressed()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }
}
