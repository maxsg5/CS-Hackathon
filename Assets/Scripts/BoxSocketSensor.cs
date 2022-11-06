using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSocketSensor : MonoBehaviour
{
    public bool isTriggered = false;
    private PlayerController playerController;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerController.isHoldingBox)
        {
            isTriggered = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTriggered = false;
        }
    }
}
