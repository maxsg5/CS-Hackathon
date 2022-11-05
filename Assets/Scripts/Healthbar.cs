using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    public void MaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void CurrentHealth(float health)
    {
        slider.value = health;
    }
    public void RemoveHealth(float health, float damage)
    {
        slider.value = health - damage;
    }
}
