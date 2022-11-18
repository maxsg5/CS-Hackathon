using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider slider;
    private float _health;
    public  float _maxHealth;

    void Start()
    {
        _health = _maxHealth;
        slider.value = _health;
    }

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }
    }

    public void RemoveHealth(float amount)
    {
        _health -= amount;
        slider.value = _health;
        if (_health - amount >= 0)
        {
            _health -= amount;
            slider.value = _health;
        }
        else
        {
            _health = 0;
            slider.value = _health;
        }
    }
    public void AddHealth(float amount)
    {
        if (_health + amount <= _maxHealth)
        {
            _health += amount;
            slider.value = _health;
        }
        else
        {
            _health = _maxHealth;
            slider.value = _health;
        }
    }

}
