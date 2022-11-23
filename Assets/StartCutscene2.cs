using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene2 : MonoBehaviour
{
    public PlayerController player;
    
    public Animator cameraAnimator;
    public AudioClip cutsceneAudio;
    public GameObject hint;
    public AudioSource levelMusic;

    [SerializeField] GameObject UIusb;
    [SerializeField] float cutsceneTime = 10.8f;
    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //fade out level music
            levelMusic.volume = 0.1f;
            player.CanMove = false;
            cameraAnimator.SetBool("cutscene2", true);
            //audioSource.PlayOneShot(cutsceneAudio); //audio clip is 10.8 seconds long, start moving bridge at 4 seconds
            hint.SetActive(true);
            StartCoroutine(Cutscene());

            //disable the collider so it only triggers once
            GetComponent<Collider2D>().enabled = false;
            
        }
    }

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(cutsceneTime); 
        cameraAnimator.SetBool("cutscene2", false);
        hint.SetActive(false);
        UIusb.SetActive(true);
        player.CanMove = true;
        //fade in level music
        levelMusic.volume = 0.5f;
    }

   
}
