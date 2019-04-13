using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{

    [SerializeField]
    public bool isFallen;

    // public event to notify controller pin was hit to update score
    public delegate void PinHit(Pin pin);
    public static PinHit PinHitEvent;

    private float startingPos;


    // Start is called before the first frame update
    void Start()
    {
        // get initial y value
        startingPos = this.transform.position.z;
        this.isFallen = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        // if there is any collision wait 3 secs then check the position of the pin
        StartCoroutine(checkPin());
    }


    IEnumerator checkPin()
    {

        //Wait for 3 seconds to allow pins to settle
        yield return new WaitForSeconds(3);

        // if hasn't been recorded as fallen before & starting pos has changed let's just say it fell
        if (!this.isFallen && (this.transform.position.z > startingPos + 0.3 || this.transform.position.z < startingPos - 0.3))
        {
            PublishPinHitEventEvent();
            this.isFallen = true; //set pin as fallen
        }


    }

    // let the everyone listening to this event know that a pin was knocked
    private void PublishPinHitEventEvent()
    {
        if (PinHitEvent != null)
        {
            PinHitEvent(this);
        }
    }


}
