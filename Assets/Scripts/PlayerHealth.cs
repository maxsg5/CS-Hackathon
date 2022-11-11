using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This can health class can be added to enemies as well as the player
public class PlayerHealth
{
    public float _currentHealth;
    public float _maxHealth;

    public float Health
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
        }
    }
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    // Constructor
    public PlayerHealth(float health, float maxHealth)
    {
        _currentHealth = health;
        _maxHealth = maxHealth;
    }
    // Methods

    // This removes health
    public void RemoveHealth(float dmgAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= dmgAmount;
        }
    }
    // This adds health
    public void AddHealth(float healAmount)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += healAmount;
        }
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}