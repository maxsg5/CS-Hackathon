using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume_Slider : MonoBehaviour
{
    public Slider slider;
    public float currentVolume;
 
    void Start()
    {
        currentVolume = slider.maxValue;
    }

    void Update()
    {
        currentVolume = slider.value;
        AdjustVolume(currentVolume);
    }

    public void AdjustVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }
}