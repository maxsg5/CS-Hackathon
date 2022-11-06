using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// when the player gets near the box, a message is displayed
public class InteractableBox : MonoBehaviour
{
    public CircleSensor circleSensor;
    public GameObject Message;

    private bool isBeingHeld = false;
    

    void Update()
    {
        if(isBeingHeld)
            return;
        if (circleSensor.isTriggered)
        {
            Message.SetActive(true);
        }
        else
        {
            Message.SetActive(false);
        }
    }
}
