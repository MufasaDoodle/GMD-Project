using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject[] DontDestroy;

    private void Start()
    {
        GameObject[] toDestroy = (FindObjectsOfType<GameObject>() as GameObject[]);
        foreach (var obj in toDestroy)
        {
            foreach (var obj2 in DontDestroy)
            {
                if (obj.name == obj2.name)
                {
                    return;
                }
            }
            GameObject.Destroy(obj);
        }
    }
    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
