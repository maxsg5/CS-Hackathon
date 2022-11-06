using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public InteractableBox interactableBox;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public Transform player, objHolder;

    public float pickUpRange = 2f;

    public bool isHolding;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!isHolding && distanceToPlayer.magnitude <= pickUpRange && interactableBox.circleSensor.isTriggered)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isHolding = true;
                rb.isKinematic = true;
                boxCollider.isTrigger = true;
                transform.SetParent(objHolder);
                transform.localPosition = Vector3.zero;
                //box carries momentum of player
                rb.velocity = player.GetComponent<Rigidbody2D>().velocity;
                //make box rotation match player rotation
                transform.rotation = player.rotation;
                //turn off the message
                interactableBox.Message.SetActive(false);
                interactableBox.enabled = false;

            }
        }
        else if(isHolding)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isHolding = false;
                rb.isKinematic = false;
                boxCollider.isTrigger = false;
                transform.SetParent(null);
                interactableBox.enabled = true;
                interactableBox.Message.SetActive(true);
            }
        }
    }
}
