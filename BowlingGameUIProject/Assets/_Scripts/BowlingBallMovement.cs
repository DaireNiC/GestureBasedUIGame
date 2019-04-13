using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBallMovement : MonoBehaviour
{
    public float force;
    public float curve;
    private float spin = 1;
    private float angle = 0;
    private List<Vector3> pinPositions;
    private List<Quaternion> pinRotations;
    private Vector3 ballPosition;
    public float ballRotationY;
    private int stage = 0;
    public bool isThrown = false;
    public GameObject box;
    public AudioSource audioData;
    private bool isPinsHit = false;




    void Start()
    {
        // gets the pins in the game and stores their positions and rotations
        var pins = GameObject.FindGameObjectsWithTag("Pin");
        pinPositions = new List<Vector3>();
        pinRotations = new List<Quaternion>();
        foreach (var pin in pins)
        {
            pinPositions.Add(pin.transform.position);
            pinRotations.Add(pin.transform.rotation);
        }

        ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position;

        audioData = GetComponent<AudioSource>();

    }


    // Update is called once per frame
    void Update()
    {
        if (isThrown == false)
        {
               var ball = GameObject.FindGameObjectWithTag("Ball");
               ball.transform.position = box.transform.position;
        }

        // adds the illusion of a spin to the ball (DO NOT EVER TOUCH THIS)
        if (spin > 0)
        {
            GetComponent<Rigidbody>().AddForce(spin, 0, 0);

            if (spin - 200 < 0)
                spin = 0;
            else
                spin = spin - 200;
        }
        else if (spin < 0)
        {
            GetComponent<Rigidbody>().AddForce(spin, 0, 0);

            if (spin + 200 > 0)
                spin = 0;
            else
                spin = spin + 200;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StageOne();
            StageTwo(0);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StageOne()
    {
        GameObject.FindGameObjectWithTag("Arrow").GetComponent<ArrowMovement>().isMoving = false;
        ballRotationY = GameObject.FindGameObjectWithTag("Arrow").GetComponent<Transform>().position.x;
    }

    public void StageTwo(float rotation)
    {
        spin = (-rotation * 10000);
        ballRotationY =  transform.position.x - ballRotationY;
        GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().velocity = Vector3.zero;
        GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(new Vector3(ballRotationY * (force / 100), 0, force));
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().countdownTimer = 6;
        stage = 0;
        isPinsHit = true;

    }


    // resets the remaining pins after the first throw
    public void ResetRemainingPins()
    {
        var pins = GameObject.FindGameObjectsWithTag("Pin");

        for (int i = 0; i < pins.Length; i++)
        {
            var pinPhysics = pins[i].GetComponent<Rigidbody>();
            pinPhysics.velocity = Vector3.zero;
            pinPhysics.position = pinPositions[i];
            pinPhysics.rotation = pinRotations[i];
            pinPhysics.velocity = Vector3.zero;
            pinPhysics.angularVelocity = Vector3.zero;

            if (pins[i].GetComponent<Pin>().isFallen == true)
                pins[i].GetComponent<Renderer>().enabled = false;
            else
                pins[i].GetComponent<Pin>().isFallen = false;

            var ball = GameObject.FindGameObjectWithTag("Ball");
            ball.transform.position = ballPosition;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    // reset the remaining pins after second throw
    public void ResetRound()
    {
        var pins = GameObject.FindGameObjectsWithTag("Pin");

        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].SetActive(true);
            var pinPhysics = pins[i].GetComponent<Rigidbody>();
            pinPhysics.velocity = Vector3.zero;
            pinPhysics.position = pinPositions[i];
            pinPhysics.rotation = pinRotations[i];
            pinPhysics.velocity = Vector3.zero;
            pinPhysics.angularVelocity = Vector3.zero;
            pins[i].GetComponent<Pin>().isFallen = false;
            pins[i].GetComponent<Renderer>().enabled = true;


            var ball = GameObject.FindGameObjectWithTag("Ball");
            ball.transform.position = ballPosition;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    // for adding sound later on
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pin" && isPinsHit == true)
        {
            audioData.Play(0);
            isPinsHit = false;
        }
    }
}