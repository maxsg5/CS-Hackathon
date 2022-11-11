using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //respawn the player if they fall off the map
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.gameManager.RemoveHealth(GameManager.gameManager._playerHealth.Health);
        }

        //respawn the box if it falls off the map
        if (collision.gameObject.CompareTag("Box"))
        {
            collision.gameObject.GetComponent<InteractableBox>().Respawn();
        }

    }
}
