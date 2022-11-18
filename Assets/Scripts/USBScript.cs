using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBScript : MonoBehaviour
{
    private USBCollected usbCollected;

    void Awake()
    {
        usbCollected = GameObject.FindGameObjectWithTag("Counter").GetComponent<USBCollected>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            usbCollected.AddScore();
            Destroy(gameObject);
        }
    }
}
