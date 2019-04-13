using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    [SerializeField]
    private int numPinsHit;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private bool isSecondRound;
    public float countdownTimer;

 



    // public event to notify ui of current roll score
    public delegate void RollScore(string score, bool isSecondRound);
    public static RollScore RollScoreEvent;
    // public event to notify ui frame score
    public delegate void FrameScore(string score, bool isSecondRound);
    public static FrameScore FrameScoreEvent;


    // Start is called before the first frame update
    void Start()
    {
        this.numPinsHit = 0;
        this.countdownTimer = -1;
        this.isSecondRound = false;
     //   startMenu

    }






        // Update is called once per frame
        void Update()
    {
        // countdowntimer is used for automatic reset
        countdownTimer -= Time.deltaTime;

        // when the countdowntimer has ran out, trigger this
        if (countdownTimer < 0 && countdownTimer > -1)
        {
            // this pushes the number of pins hit to the scoring system which stores the numpinshit value
            this.GetComponent<ScoringSystem>().Roll(numPinsHit);
            
         

            // change the timer to -1 so it stops triggering
            countdownTimer = -1;

            // to ensure the next bracket wont be reset with empty pins if the player hits a strike 
            if (isSecondRound == false && numPinsHit == 10)
            {
                isSecondRound = true;
            }
         

            // checks if current throw is first or second round, if first round, make sure the remainer pins are set, if second round, make sure all pins are reset
            if (isSecondRound == true)
            {

                // reset all pins, and push the current score to the text box
                GameObject.FindGameObjectWithTag("Ball").GetComponent<BowlingBallMovement>().ResetRound();
                // send the score and whether is second throw or not
                PublishFrameScoreEvent(this.GetComponent<ScoringSystem>().Score() + "", isSecondRound);
                // send the roll score to UI elements
                PublishRollScoreEvent(numPinsHit.ToString(), isSecondRound);
                isSecondRound = false;
                //scoreText.text = "Score: " + this.GetComponent<ScoringSystem>().Score();
               
              
            }
            else
            {
       
                // reset pins that haven't been hit down
                GameObject.FindGameObjectWithTag("Ball").GetComponent<BowlingBallMovement>().ResetRemainingPins();
                // send the roll score to UI elements
                PublishRollScoreEvent(numPinsHit.ToString(), isSecondRound);
                isSecondRound = true;
            }
            numPinsHit = 0;

            // enable the arrow movement after each throw
            GameObject.FindGameObjectWithTag("Arrow").GetComponent<ArrowMovement>().isMoving = true;

            GameObject.FindGameObjectWithTag("Ball").GetComponent<BowlingBallMovement>().isThrown = false;

            GameObject.FindGameObjectWithTag("Myo").GetComponent<JointOrientation>().ResetMovements();

        }


    }

    // subscribe to events here
    private void OnEnable()
    {
        // listening for when a pin registers as knocked
        Pin.PinHitEvent += HandlePinHitEvent;
    }
    private void OnDisable()
    {
        Pin.PinHitEvent -= HandlePinHitEvent;
    }

    private void HandlePinHitEvent(Pin pin)
    {
        this.numPinsHit++;
        // update score once pin knocked
        Debug.Log("hit!" + numPinsHit);
    }


    // publish total score from frame 
    private void PublishFrameScoreEvent(string score, bool isSecondRound)
    {
        if (FrameScoreEvent != null)
        {
            FrameScoreEvent(score, isSecondRound);
        }
    }

    // publish score from roll 
    private void PublishRollScoreEvent(string score, bool isSecondRound)
    {
        if (RollScoreEvent != null)
        {
            RollScoreEvent(score, isSecondRound);
        }
    }

}
