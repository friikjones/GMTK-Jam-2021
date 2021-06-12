using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LapController : MonoBehaviour
{
    private List<Transform> gatesList;
    public List<float> lapTimers, splitsList;

    public bool activeGateCrossed;
    public int currentGate, currentLap;
    public int lapTotal;

    public float currentTotal, currentLapSplit, currentZoneSplit;

    public TextMeshProUGUI lapText, timerText;

    // Start is called before the first frame update
    void Start()
    {
        currentTotal = 0;
        currentLap = 0;
        currentGate = 0;

        gatesList = new List<Transform>();
        foreach (Transform child in transform)
        {
            gatesList.Add(child);
        }

        RefreshGates();
    }

    // Update is called once per frame
    void Update()
    {
        currentTotal += Time.deltaTime;
        currentLapSplit += Time.deltaTime;
        currentZoneSplit += Time.deltaTime;

        lapText.SetText("Lap {0}/{1}", (currentLap + 1), lapTotal);
        timerText.SetText("{0:2}s", currentTotal);


        if (activeGateCrossed)
        {
            Debug.Log("New Gate");
            currentGate++;
            activeGateCrossed = false;
            splitsList.Add(currentZoneSplit);
            currentZoneSplit = 0;

            if (currentGate == gatesList.Count)
            {
                Debug.Log("New Lap");
                currentLap++;
                currentGate = 0;
                lapTimers.Add(currentLapSplit);
                currentLap = 0;

                if (currentLap == lapTotal)
                {
                    Debug.Log("Track done");
                    // Ends the track
                }
            }

            RefreshGates();
        }
    }

    void RefreshGates()
    {
        foreach (Transform gate in gatesList)
        {
            gate.GetComponent<BoxCollider>().enabled = false;
        }
        gatesList[currentGate].GetComponent<BoxCollider>().enabled = true;

    }
}
