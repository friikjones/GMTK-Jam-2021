using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{

    public List<float> lapTimers, splitsList;
    public float totalTimer;

    public TextMeshProUGUI totalTimeText, lapTimeText;

    public float bestTotalTime, bestLapTime;

    public GameObject bestTotal, bestLap;

    private void OnEnable()
    {
        // string currentScene = SceneManager.GetActiveScene().name;
        // bestTotalTime = PlayerPrefs.GetFloat(currentScene + "_bestTime", 1000000);
        // bestLapTime = PlayerPrefs.GetFloat(currentScene + "_bestLap", 1000000);

        LapController lapController = GameObject.Find("LapController").GetComponent<LapController>();
        lapTimers = lapController.lapTimers;
        totalTimer = lapController.currentTotal;

        lapTimers.Sort();


        //TODO Format this garbage properly
        var timerMinutes = (totalTimer / 60) % 1;
        var timerSeconds = totalTimer - timerMinutes * 60;
        string totalTimeString = String.Format("{0}\'{1:3}}", timerMinutes, timerSeconds);
        //TODO Format this garbage properly
        var lapMinutes = (lapTimers[0] / 60) % 1;
        var lapSeconds = lapTimers[0] - lapTimers[0] * 60;
        string lapTimeString = String.Format("{0}\'{1:3}}", lapMinutes, lapSeconds);


        totalTimeText.text = totalTimeString;
        lapTimeText.text = lapTimeString;

        // if (totalTimer < bestTotalTime)
        // {
        //     PlayerPrefs.SetFloat(currentScene + "_bestTime", totalTimer);
        //     bestTotal.SetActive(true);
        // }
        // if (lapTimers[0] < bestLapTime)
        // {
        //     PlayerPrefs.SetFloat(currentScene + "_bestLap", totalTimer);
        //     bestLap.SetActive(true);
        // }
    }

}
