using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public float totalTimer;

    public TextMeshProUGUI totalTimeText, lapTimeText;

    public float bestLapTime;

    public LapController lap;

    private void Update()
    {
        totalTimer = lap.currentTotal;
        bestLapTime = lap.bestLap;

        totalTimeText.SetText("{0:2}s", totalTimer);
        lapTimeText.SetText("{0:2}s", bestLapTime);
    }

}
