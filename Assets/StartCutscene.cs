using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public PlayerController player;
    public Animator cameraAnimator;
    public AudioClip cutsceneAudio;
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
            player.DisableControl();
            cameraAnimator.SetBool("cutscene1", true);
            audioSource.PlayOneShot(cutsceneAudio); //audio clip is 10.8 seconds long
            StartCoroutine(Cutscene1());
        }
    }

    IEnumerator Cutscene1()
    {
        yield return new WaitForSeconds(10.8f);
        cameraAnimator.SetBool("cutscene1", false);
        player.EnableControl();
    }
}
