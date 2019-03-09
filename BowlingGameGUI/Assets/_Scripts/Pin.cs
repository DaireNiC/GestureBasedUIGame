using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.transform.up.y < 70)
        {
          // Debug.Log("pin fell");
        }



        if (transform.up.y < 0.0)
        {
            // Fallen 1.0 (upright) and 0.0 (flat on ground).
            Debug.Log("pin fell");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Plane")
        {
          //S  Debug.Log("pin fell");
        }
    }
}
