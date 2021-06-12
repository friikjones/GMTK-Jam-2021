using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public bool leftCarThrough, rightCarThrough;

    private LapController lapController;

    private void Start()
    {
        lapController = transform.parent.GetComponent<LapController>();
    }

    private void FixedUpdate()
    {
        if (leftCarThrough && rightCarThrough)
        {
            lapController.activeGateCrossed = true;
            leftCarThrough = rightCarThrough = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.name == "Car left")
                leftCarThrough = true;
            else
                rightCarThrough = true;
        }
    }

}
