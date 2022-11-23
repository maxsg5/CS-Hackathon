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

    [SerializeField] GameObject EndLevelUI;
    [SerializeField] int unlockScore = 10;
    [SerializeField] PlayerController player;
    private BoxCollider2D boxCollider2D;
    void Awake()
    {
        TextPrompt.text = (unlockScore - usbCollected.GetScore()).ToString();
        boxCollider2D = GetComponent<BoxCollider2D>();
    
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
            //change collider to trigger
            boxCollider2D.isTrigger = true;
            


        }
        
        
    }



    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EndLevelUI.SetActive(true);
            player.CanMove = false;
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
   
}
