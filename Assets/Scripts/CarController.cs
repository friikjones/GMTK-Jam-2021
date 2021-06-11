using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject otherCar;
    public KeyCode keyForward, keyBackward, keyTurnAnti, keyTurnClock;
    public KeyCode keyTether;

    private Rigidbody rb;
    private float defaultDrag;

    public float speedAcel, speedBack, dragBreak;
    public float speedTurn;
    public float topSpeed, topSpeedBack;
    public float pullForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultDrag = rb.drag;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
    }

    void GetInputs()
    {
        if (Input.GetKey(keyForward))
        {
            MoveForward();
        }
        if (Input.GetKey(keyBackward))
        {
            MoveBackward();
        }
        else
        {
            ReleaseBackward();
        }
        if (Input.GetKey(keyTurnAnti))
        {
            MoveTurnAnti();
        }
        if (Input.GetKey(keyTurnClock))
        {
            MoveTurnClock();
        }
        if (Input.GetKey(keyTether))
        {
            ActiveTether();
        }
    }

    void MoveForward()
    {
        var locVel = transform.InverseTransformDirection(rb.velocity);

        if (locVel.z < topSpeed)
            rb.velocity += transform.forward * speedAcel;
        Debug.Log("key forward, " + this.name);
    }

    void MoveBackward()
    {
        var locVel = transform.InverseTransformDirection(rb.velocity);
        if (locVel.z > 0)
            // rb.velocity -= transform.forward * speedBrake;
            rb.drag = dragBreak;
        if (locVel.z <= 0 && locVel.z > -topSpeedBack)
            rb.velocity -= transform.forward * speedBack;

        Debug.Log("key backward, " + this.name);
    }
    void ReleaseBackward()
    {
        rb.drag = defaultDrag;
    }

    void MoveTurnAnti()
    {
        transform.RotateAround(transform.position, Vector3.up, -speedTurn * Time.deltaTime);
        Debug.Log("key anti, " + this.name);
    }

    void MoveTurnClock()
    {
        transform.RotateAround(transform.position, Vector3.up, speedTurn * Time.deltaTime);
        Debug.Log("key clock, " + this.name);
    }

    void ActiveTether()
    {
        rb.velocity += transform.LookAt(otherCar) * pullForce;
        Debug.Log("key teher, " + this.name);
    }
}
