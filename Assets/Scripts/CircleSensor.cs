using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSensor : MonoBehaviour
{
    public bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
