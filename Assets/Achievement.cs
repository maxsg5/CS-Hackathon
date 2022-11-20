using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    [SerializeField] AudioClip achievementSound;

    private AudioSource audioSource;
    private Animator animator;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

    }

    public void PopAchievement()
    {
        animator.SetTrigger("pop");
        audioSource.PlayOneShot(achievementSound);
    }
}
