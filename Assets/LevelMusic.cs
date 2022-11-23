using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    
    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FadeVolume(float length)
    {
        StartCoroutine(VolumeFade(length));
    }
    IEnumerator VolumeFade(float length)
    {
        audioSource.volume = 0.2f;
        yield return new WaitForSeconds(length);
        audioSource.volume = 0.5f;
    }
}
