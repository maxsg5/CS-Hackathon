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
    
    private Transform spawnPoint;
    private DissolveController dissolveController;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPoint = transform;
        dissolveController = GetComponent<DissolveController>();
    }
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
                player.GetComponent<PlayerController>().isHoldingBox = true;

            }
        }
        else if(isHolding)
        {
            return;
        }
    }
    
    public void DropItem()
    {
        isHolding = false;
        rb.isKinematic = false;
        boxCollider.isTrigger = false;
        transform.SetParent(null);
        player.GetComponent<PlayerController>().isHoldingBox = false;
    }

    public void DestroyBox()
    {
        //dissolve the box
        dissolveController.isDissolving = true;
        StartCoroutine(ResetBoxCo());

    }

    private void ResetBox()
    {
        //reset the box
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        transform.SetParent(null);
        rb.isKinematic = false;
        boxCollider.isTrigger = false;
        isHolding = false;
        player.GetComponent<PlayerController>().isHoldingBox = false;
        //reset the interactable box
        interactableBox.enabled = true;
    }
    
    IEnumerator ResetBoxCo()
    {
        yield return new WaitForSeconds(2f);
        ResetBox();
        dissolveController.isDissolving = false;
    }
}
