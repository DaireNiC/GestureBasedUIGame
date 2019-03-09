using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    // constant for pin name
    private const string PIN = "Pin";

    // List of bowling pins
    [SerializeField]
    private IList<GameObject>pins;

    // Start is called before the first frame update
    void Start()
    {
        // get all the bowling pins & add to list
        this.pins = GameObject.FindGameObjectsWithTag(PIN);
       
    }

    // Update is called once per frame
    void Update()
    {
        /*     foreach (GameObject p in this.pins)
              {

                  Debug.Log("");
              }
          }
          */
    }
}
