using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTracker : MonoBehaviour
{
    public GameObject cameraTracker;
    public GameObject leftCar, rightCar;
    // Start is called before the first frame update
    void Start()
    {
        var found = GameObject.FindGameObjectsWithTag("Player");
        leftCar = found[0];
        rightCar = found[1];
    }

    // Update is called once per frame
    void Update()
    {
        TrackerPositionUpdate();
        CameraLookAt();
    }

    void TrackerPositionUpdate()
    {
        cameraTracker.transform.position = (leftCar.transform.position + rightCar.transform.position) / 2;
    }
    void CameraLookAt()
    {
        this.transform.LookAt(cameraTracker.transform.position);
    }
}
