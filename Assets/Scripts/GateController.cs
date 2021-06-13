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
        Debug.Log("Passed " + this.name);
        Debug.Log("by " + other.name + " with tag " + other.tag);

        if (other.tag == "Player")
        {
            if (other.name == "Car Left")
                leftCarThrough = true;
            else
                rightCarThrough = true;
        }
    }

}
