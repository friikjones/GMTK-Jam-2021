using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CarControllerRedo : MonoBehaviour
{
    public GameObject otherCar;
    public KeyCode keyForward, keyBackward, keyTurnAnti, keyTurnClock, keyTether;

    public Rigidbody rb, otherRb;

    public float forwardAccel, reverseAccel, maxSpeed, turnStrength, gravityForce;
    public float dragOnGround, dragOnAir;
    public float pullForce, pullSelfMultiplier;
    private float speedInput, turnInput;
    public bool grounded;

    public float carDist, maxRange, minRange;

    public LayerMask whatIsGround;
    public float groundRayLength = .75f;
    public Transform groundRayPoint;

    public AudioSource magnetSound;


    private Transform thisMagnet, otherMagnet;

    void Start()
    {
        rb.transform.parent = null;
        // Physics.IgnoreLayerCollision(7, 7);
        otherRb = otherCar.GetComponent<CarControllerRedo>().rb;
        //magnets
        thisMagnet = transform.GetChild(0);
        otherMagnet = otherCar.transform.GetChild(0);
    }

    private void Update()
    {
        GetInputs();
        MagnetTarget();

        if (grounded)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles
                                                + new Vector3(0f, turnInput * turnStrength * Time.deltaTime, 0f));
        transform.position = rb.transform.position;

        magnetSound.volume = (carDist / 10) / 2;

    }

    void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        if (grounded)
        {
            rb.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                rb.AddForce(transform.forward * speedInput * forwardAccel * 1000f);
            }
        }
        else
        {
            rb.drag = dragOnAir;
            rb.AddForce(Vector3.up * -gravityForce * 100);
        }

        ActiveTether();

    }

    void GetInputs()
    {
        speedInput = 0;
        turnInput = 0;
        if (Input.GetKey(keyForward))
        {
            speedInput = 1;
        }
        else if (Input.GetKey(keyBackward))
        {
            speedInput = -1;
        }
        if (Input.GetKey(keyTurnAnti))
        {
            turnInput = -1;
        }
        if (Input.GetKey(keyTurnClock))
        {
            turnInput = 1;
        }

        // if (Input.GetKey(keyTether))
        // {
        //     ActiveTether();
        // }
    }

    //Magnet and Tether
    void MagnetTarget()
    {
        thisMagnet.LookAt(otherMagnet);
    }
    void ActiveTether()
    {
        Vector3 dist = otherRb.position - rb.position;
        carDist = dist.magnitude;

        float multiplier = 0;

        if (carDist > maxRange)
        {
            multiplier = 3;
        }
        else if (carDist > minRange)
        {
            multiplier = carDist / maxRange;
        }

        otherRb.AddForce(otherMagnet.transform.forward * pullForce * 1000 * multiplier);
        rb.AddForce(thisMagnet.transform.forward * pullForce * pullSelfMultiplier * 1000 * multiplier);
        // var target = transform.LookAt(otherCar.transform.position)
    }



}
