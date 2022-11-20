using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndLevelPortal : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshPro TextPrompt;
    [SerializeField] GameObject USBImage;
    [SerializeField] USBCollected usbCollected;
    [SerializeField] Color color;

    [SerializeField] int unlockScore = 10;
    void Awake()
    {
        TextPrompt.text = (unlockScore - usbCollected.GetScore()).ToString();
    
    }

    
    public void UpdateScore()
    {
        TextPrompt.text = (unlockScore - usbCollected.GetScore()).ToString();

        if (usbCollected.GetScore() >= unlockScore)
        {
            TextPrompt.text = string.Empty;
            USBImage.SetActive(false);

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
