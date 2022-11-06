using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// when the player gets near the box, a message is displayed
public class InteractableBox : MonoBehaviour
{
    public CircleSensor circleSensor;
    public BoxSocketSensor boxSocketSensor;
    public GameObject Message;
    public GameObject Socket;

    private bool isBeingHeld = false;
    private PickUpController pickUpController;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pickUpController = GetComponent<PickUpController>();
    }


    void Update()
    {
        if(isBeingHeld)
            return;

        if (boxSocketSensor.isTriggered)
        {
            pickUpController.enabled = false;
            rb.isKinematic = true;
            //detach the box from the player
            transform.SetParent(null);
            isBeingHeld = false;
            
            //move the box towards the socket using velocity
            rb.velocity = (Socket.transform.position - transform.position).normalized * 5f;


            return;
        }
        
        pickUpController.enabled = true;
        rb.isKinematic = false;
        
        
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
