using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    // little squares tht show the score
    public GameObject prefab;
    // num squares in the score grid
    public int numberToCreate; 
    private GameObject text;

    // the array of score values in the score grid
    private GameObject[] scoreSquare;


    // two scenarios --> displaying row score || frame score
    [SerializeField]
    private Boolean isFrameScore;
    // current position in the grid of scores 
    private int textPosition;


    void Start()
    {

        scoreSquare = new GameObject[numberToCreate]; 
                                                
        Populate();

        textPosition = 0;
    }

    void Update()
    {
    }

    void Populate()
    {


        for (int i = 0; i < numberToCreate; i++)
        {
            // create the squares in the score grid
            text = (GameObject)Instantiate(prefab, transform);
          //  text.GetComponent<Image>().color = new Color(255, 255, 255, 255);

            // add each score to array for future ref to update values
            scoreSquare[i] = text;

        }

    }

    // subscribe to events here
    private void OnEnable()
    {
        // listening for when the game controller updates the score
        if (isFrameScore)
        {
            GameControl.FrameScoreEvent += HandleFrameScoreEvent;
        }
        else
        {
            GameControl.RollScoreEvent += HandleRollScoreEvent;
        }

    }



    private void OnDisable()
    {

        // listening for when the game controller updates the score
        if (isFrameScore)
        {
            GameControl.FrameScoreEvent -= HandleFrameScoreEvent;
        }
        else
        {
            GameControl.RollScoreEvent -= HandleRollScoreEvent;

        }
    }

    private void HandleRollScoreEvent(string score, bool isSecondRound)
     {
       
        Debug.Log("ROLL SORE EVENT  " + score);
        // if the score is a strike write an X in the score box
        // if isSecondRound and previous score was "X" then skip this box

        if (score.Equals("10"))
        {
            Debug.Log("Strike!  " + score);
            //update blank to indicate strike
            score = "X";
        }

        // if isSecondRound and previous score was "X" then skip this box
        if (isSecondRound)
        {
            // if isnt first box and previous score a strike --> skip and write a blank 
            if (textPosition > 0) { 
                if ((scoreSquare[(textPosition - 1)].GetComponentInChildren<Text>().text.Equals("X")) == true)
                {
                    Debug.Log("last score was a strike adding in Blank");
                    score = " ";
                }
            }
           
        }


        updateScorePanel(score);
        Debug.Log("Event triggered. score has been updated by the controller -->  " + score);
        textPosition++;


    }

    private void HandleFrameScoreEvent(string score, bool isSecondRound)

    {
        updateScorePanel(score);
        Debug.Log("Event triggered. score has been updated by the controller -->  " + score);
        textPosition++;

    }





    private void updateScorePanel(string score)
    {
        print(scoreSquare[textPosition].GetComponentInChildren<Text>().text);
        scoreSquare[textPosition].GetComponentInChildren<Text>().text = score;
    }
}