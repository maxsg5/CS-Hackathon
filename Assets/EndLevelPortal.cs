using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelPortal : MonoBehaviour
{

    [SerializeField] GameObject TextPrompt;
    [SerializeField] GameObject USBImage;
    [SerializeField] USBCollected usbCollected;
    [SerializeField] Color color;

    int unlockScore = 10;
    void Awake()
    {
        TextPrompt.SetActive(false);
    
    }

    
    public void UpdateScore()
    {
        TextPrompt.transform.GetChild(0).GetComponent<Text>().text = (unlockScore - usbCollected.GetScore()).ToString();

        if (usbCollected.GetScore() >= unlockScore)
        {
            TextPrompt.SetActive(false);
            USBImage.SetActive(true);

            Material mat = GetComponent<Renderer>().material;
            mat.SetColor("_Color",color);
            mat.SetFloat("_scale", 0.5f);

        }
        
        
    }



    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //TODO: end the game...
        }
    }
   
}
