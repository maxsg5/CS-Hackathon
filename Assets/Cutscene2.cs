using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene2 : MonoBehaviour
{
    public PlayerController player;
    public Animator cameraAnimator;

    public float cutsceenLength = 5f;    

    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void StartCutscene()
    {
        //TODO: Disable player movement
        cameraAnimator.SetBool("cutscene1", true);
        StartCoroutine(Cutscene1());
    }

    IEnumerator Cutscene1()
    {
        yield return new WaitForSeconds(cutsceenLength);
        cameraAnimator.SetBool("cutscene1", false);
    }

}
