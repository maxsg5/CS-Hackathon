using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public PlayerController player;
    public Animator cameraAnimator;
    public AudioClip cutsceneAudio;
    public Bridge bridge;
    public GameObject interactableBox;
    public GameObject hint;
    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //turn off player control
            //GameManager.gameManager.RemoveHealth(GameManager.gameManager._playerHealth.Health);
            player.CanMove = false;
            cameraAnimator.SetBool("cutscene1", true);
            audioSource.PlayOneShot(cutsceneAudio); //audio clip is 10.8 seconds long, start moving bridge at 4 seconds
            StartCoroutine(Cutscene1());
            StartCoroutine(MoveBridge());
            StartCoroutine(ShowBox());
            //disable the collider so it only triggers once
            GetComponent<Collider2D>().enabled = false;
            

        }
    }

    IEnumerator Cutscene1()
    {
        yield return new WaitForSeconds(10.8f);
        cameraAnimator.SetBool("cutscene1", false);
        hint.SetActive(false);
        player.CanMove = true;
    }

    IEnumerator MoveBridge()
    {
        yield return new WaitForSeconds(4f);
        bridge.MoveBridge();
    }

    IEnumerator ShowBox()
    {
        yield return new WaitForSeconds(7f);
        interactableBox.SetActive(true);
        hint.SetActive(true);
    }
}
