using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    public int maxFramerate = 60;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = maxFramerate;
        QualitySettings.vSyncCount = 0;
    }
}
