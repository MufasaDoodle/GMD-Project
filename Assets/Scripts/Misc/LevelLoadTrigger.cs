using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadTrigger : MonoBehaviour
{
    public string levelToLoad;
    public Vector3 startingPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            return;
        }

        PlayerManager.Instance.PlayerEquipment.gameObject.GetComponent<Transform>().position = startingPoint;
        SceneManager.LoadScene(levelToLoad);
    }
}
