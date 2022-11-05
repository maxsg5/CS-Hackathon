using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// when the player gets near the box, a message is displayed
public class InteractableBox : MonoBehaviour
{
    [SerializeField] private CircleSensor circleSensor;
    [SerializeField] private GameObject Message;

    private bool isBeingHeld = false;
    

    void Update()
    {
        if(isBeingHeld)
            return;
        if (circleSensor.isTriggered)
        {
            Message.SetActive(true);
            //if the player presses the interact button, the box is moved to the player's ObjectHolder
            if (Input.GetKeyDown(KeyCode.E))            
            {
                print("interacted");
                //make the box a child of the player's ObjectHolder
                GameObject objHolder = GameObject.Find("ObjectHolder");
                transform.parent = objHolder.transform;
                //turn off the box's rigidbody and collider
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<BoxCollider2D>().enabled = false;
                //move the box to the ObjectHolder
                transform.position = objHolder.transform.position;
                Message.SetActive(false);
                isBeingHeld = true;
            }
        }
        else
        {
            Message.SetActive(false);
        }
    }
}
