using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject otherCar;
    public KeyCode keyForward, keyBackward, keyTurnAnti, keyTurnClock;
    public KeyCode keyTether;

    private Rigidbody rb, otherRb;
    private float defaultDrag;
    private Transform thisMagnet, otherMagnet;
    private int collisionTicks;

    public bool grounded;

    public float speedAcel, speedBack, dragBreak;
    public float speedTurn;
    public float topSpeed, topSpeedBack;
    public float pullForce;

    // Start is called before the first frame update
    void Start()
    {
        //rigidbodies
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        otherRb = otherCar.GetComponent<Rigidbody>();
        //drag
        defaultDrag = rb.drag;
        //magnets
        thisMagnet = transform.GetChild(0);
        otherMagnet = otherCar.transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        GetFloorContact();
        ResetIfFlipped();
        GetInputs();
        MagnetTarget();
    }

    void FixedUpdate()
    {
        collisionTicks++;
        if (collisionTicks <= 10) {
            pullForce = 0.01f;
            otherRb.mass = 2;
            topSpeed = 10;
        } else {
            pullForce = 0.2f;
            otherRb.mass = 1;
            topSpeed = 20;
        }
        
    }

    void GetFloorContact()
    {
        int hitCount = 0;
        Vector3[] raycastPoints = { new Vector3(.5f, 0, -1),
                                    new Vector3(.5f, 0, 1),
                                    new Vector3(-.5f, 0, -1),
                                    new Vector3(-.5f, 0, -1), };
        for (int i = 0; i < 4; i++)
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position + raycastPoints[i], transform.TransformDirection(Vector3.down), out hit, 10))
            {
                Debug.DrawRay(transform.position + raycastPoints[i], transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                hitCount++;
            }
            else
            {
                Debug.DrawRay(transform.position + raycastPoints[i], transform.TransformDirection(Vector3.down) * 10, Color.yellow);
            }
        }
        if (hitCount > 1)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    void ResetIfFlipped()
    {
        if (!grounded && rb.velocity.magnitude < .1f)
        {
            Vector3 currentPos = transform.position + Vector3.up;
            transform.rotation = Quaternion.identity;
            transform.position = currentPos;
        }
    }

    //Get inputs
    void GetInputs()
    {
        if (Input.GetKey(keyForward))
        {
            if (grounded)
            {
                MoveForward();
            }
        }
        if (Input.GetKey(keyBackward))
        {
            if (grounded)
            {
                MoveBackward();
            }
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

    //Car movement
    void MoveForward()
    {
        var locVel = transform.InverseTransformDirection(rb.velocity);

        if (locVel.z < topSpeed)
            rb.velocity += transform.forward * speedAcel;
    }

    void MoveBackward()
    {
        var locVel = transform.InverseTransformDirection(rb.velocity);
        if (locVel.z > 0)
            // rb.velocity -= transform.forward * speedBrake;
            rb.drag = dragBreak;
        if (locVel.z <= 0 && locVel.z > -topSpeedBack)
            rb.velocity -= transform.forward * speedBack;
    }
    void ReleaseBackward()
    {
        rb.drag = defaultDrag;
    }

    void MoveTurnAnti()
    {
        transform.RotateAround(transform.position, Vector3.up, -speedTurn * Time.deltaTime);
    }

    void MoveTurnClock()
    {
        transform.RotateAround(transform.position, Vector3.up, speedTurn * Time.deltaTime);
    }

    //Magnet and Tether
    void MagnetTarget()
    {
        thisMagnet.LookAt(otherMagnet);
    }
    void ActiveTether()
    {
        otherRb.velocity += otherMagnet.transform.forward * pullForce;
        rb.velocity -= otherMagnet.transform.forward * pullForce / 20;
        // var target = transform.LookAt(otherCar.transform.position)
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == otherCar.name) {
            // Debug.Log("cars are together");
            collisionTicks = 0;
        }
    }
}
