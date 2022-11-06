using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSocketSensor : MonoBehaviour
{
    public bool isTriggered = false;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Socket")
        {
            isTriggered = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Socket")
        {
            isTriggered = false;
        }
    }
}
