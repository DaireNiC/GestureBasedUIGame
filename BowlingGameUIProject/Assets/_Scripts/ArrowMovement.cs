using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for the direction of the arrow that controls direction of movement of ball
public class ArrowMovement : MonoBehaviour
{
    private bool isMoveRight = true;
    public float angle = 0;
    public bool isMoving = true;


    // Update is called once per frame
    void Update()
    {
        // check to rotate arrow clockwise or anticlockwise, and freeze when entering second stage
        if (isMoveRight == true && isMoving == true)
            transform.Translate(1.5f,0,0);
        else if (isMoveRight == false && isMoving == true)
            transform.Translate(-1.5f, 0, 0);

        if (transform.position.x > 80)
            isMoveRight = false;
        if (transform.position.x < -80)
            isMoveRight = true;

    }
}
