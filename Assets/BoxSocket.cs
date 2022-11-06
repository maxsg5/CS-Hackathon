using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxSocket : MonoBehaviour
{
    public BoxSocketSensor boxSocketSensor;
    public GameObject Message;
    public GameObject interactableBox;
    public UnityEvent OnBoxPlaced;

    private InteractableBox interactableBoxScript;
    private PlayerController playerController;
    private bool placed = false;
   
    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        interactableBoxScript = interactableBox.GetComponent<InteractableBox>();
    }

   
    void Update()
    {
        if(placed)
            return;
        
        //if the player is holding a box and is near the socket, display the message
        if (boxSocketSensor.isTriggered)
        {
            Message.SetActive(true);

            //if the player presses E, the box is placed in the socket
            if(Input.GetKeyDown(KeyCode.E))
            {
                placed = true;
                Message.SetActive(false);
                interactableBoxScript.enabled = false;
                interactableBox.transform.position = transform.position;
                interactableBox.transform.rotation = transform.rotation;
                //make the box a child of the socket
                interactableBox.transform.SetParent(transform);
                //turn off the box's rigidbody2d 
                interactableBox.GetComponent<Rigidbody2D>().isKinematic = true;
                interactableBox.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                interactableBox.GetComponent<Rigidbody2D>().rotation = 0f;
                //turn off the box's collider2d
                interactableBox.GetComponent<Collider2D>().enabled = false;
                playerController.isHoldingBox = false;
                //invoke the event
                OnBoxPlaced.Invoke();
            }
        }
        else
        {
            Message.SetActive(false);
        }     
    }
}
