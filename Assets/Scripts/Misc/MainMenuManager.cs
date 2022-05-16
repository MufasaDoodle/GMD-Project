using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject loadingUI;

    public void OnPlayPressed()
    {
        loadingUI.SetActive(true);
        SceneManager.LoadScene("Loading", LoadSceneMode.Single);
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }
}
