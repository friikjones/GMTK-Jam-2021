using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapController : MonoBehaviour
{
    public List<Transform> gatesList;
    public List<float> lapTimers, splitsList;

    public bool activeGateCrossed;
    public int currentGate, currentLap;
    public int lapTotal;

    public float currentTimer, currentSplit;

    // Start is called before the first frame update
    void Start()
    {
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
        currentTimer += Time.deltaTime;
        currentSplit += Time.deltaTime;

        if (activeGateCrossed)
        {
            Debug.Log("New Gate");
            currentGate++;
            activeGateCrossed = false;
            splitsList.Add(currentSplit);
            currentSplit = 0;

            if (currentGate == gatesList.Count)
            {
                Debug.Log("New Lap");
                currentLap++;
                currentGate = 0;
                lapTimers.Add(currentTimer);
                currentTimer = 0;

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
