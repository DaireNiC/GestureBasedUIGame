using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    private int[] rolls = new int[22];
    private int currentRoll = 0;


    // this stores the number of pins hit
    public void Roll(int pins)
    {
        rolls[currentRoll] = pins;
        currentRoll++;

        if (currentRoll == 22) {
           // Debug.Log("end");
           // call score!
        }
           
    }

    // this gets the current total score
    public int Score()
    {
        int score = 0;
        int roll = 0;

        // iterate through all brackets, calculate score
        for (int i = 0; i < 10; i++)
        {
            if (IsStrike(roll))
            {
                score += SumStrike(roll);
                roll++;
                Debug.Log("strike");
            }
            else if (IsSpare(roll))
            {
                score += SumSpare(roll);
                roll += 2;
                Debug.Log("spare");

            }
            else
            {
                score += SumTurn(roll);
                roll += 2;
                //Debug.Log("nothing");

            }
        }

        return score;
    }

    private int SumTurn(int roll)
    {
        return rolls[roll] + rolls[roll + 1];
    }

    private int SumSpare(int roll)
    {
        return 10 + rolls[roll + 2];
    }

    private int SumStrike(int roll)
    {
        return 10 + rolls[roll + 1] + rolls[roll + 2];
    }

    private bool IsSpare(int roll)
    {
        return rolls[roll] + rolls[roll + 1] == 10;
    }

    private bool IsStrike(int roll)
    {
        return rolls[roll] == 10;
    }

}
