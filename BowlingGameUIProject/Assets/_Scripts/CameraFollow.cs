using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// camera that follows the ball
public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    void Start()
    {
        // get the offset
        offset = transform.position;
    }
    void LateUpdate()
    {
        // follows the ball until the end of the alley
        if (player.transform.position.z < 100)
            transform.position = player.transform.position + offset;
    }
}
