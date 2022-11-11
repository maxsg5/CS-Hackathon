using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Mainly added the healthbar so its can also be accessed like the health of the player, and the health can be shown on the healthbar
    public Healthbar healthbar;
    public static GameManager gameManager { get; private set; }

    public PlayerHealth _playerHealth = new PlayerHealth(100, 100);

    // This Removes health when called
    public void RemoveHealth(float amount)
    {
        if (_playerHealth._currentHealth - amount >= 0)
        {
            _playerHealth.RemoveHealth(amount);
            healthbar.CurrentHealth(_playerHealth._currentHealth);
        } else
        {
            _playerHealth._currentHealth = 0;
            healthbar.CurrentHealth(_playerHealth._currentHealth);
        }
    }

    // This Adds health when called
    public void AddHealth(float amount)
    {
        if (_playerHealth._currentHealth + amount <= _playerHealth._maxHealth)
        {
            _playerHealth.AddHealth(amount);
            healthbar.CurrentHealth(_playerHealth._currentHealth);
        } else
        {
            _playerHealth._currentHealth = _playerHealth._maxHealth;
            healthbar.CurrentHealth(_playerHealth._currentHealth);
        }
    }

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        } else
        {
            gameManager = this;
        }
    }
}
