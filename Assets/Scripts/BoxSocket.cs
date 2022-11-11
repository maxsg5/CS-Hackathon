using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxSocket : MonoBehaviour
{
    public GameObject Message;
    public GameObject interactableBox;
    public UnityEvent OnBoxPlaced;

    private InteractableBox interactableBoxScript;
    private PlayerController playerController;
    private bool placed = false;
    private ParticleSystem ps;
   
    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        interactableBoxScript = interactableBox.GetComponent<InteractableBox>();
        ps = GetComponent<ParticleSystem>();
    }

   
    void Update()
    {
        if(placed)
            return;
        
        //if the distance between the socket and the box is less than 1 we are in the socket
        float distance = Vector3.Distance(transform.position, interactableBox.transform.position);
        if(distance < 1.5f)
        {
            //turn off the box's rigidbody and collider
            interactableBox.GetComponent<Rigidbody2D>().isKinematic = true;
            interactableBox.GetComponent<BoxCollider2D>().enabled = false;
            //set velocity to 0
            interactableBox.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //set the box's position to the socket's position
            interactableBox.transform.position = transform.position;
            //set the box's rotation to the socket's rotation
            interactableBox.transform.rotation = transform.rotation;
            //set the box's parent to the socket
            interactableBox.transform.parent = transform;
            //turn the interactable box message off
            interactableBoxScript.Message.SetActive(false);
            //turn off the interactable box script
            interactableBoxScript.enabled = false;
            placed = true;
            //turn on the particle system
            ps.Play();
            //invoke the event
            OnBoxPlaced.Invoke();
        }


    }
}
