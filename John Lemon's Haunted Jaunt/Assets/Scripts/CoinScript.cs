using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float _maxHealth = 1;
    public float _currentHealth;
    private HealthBar _healthbar;

    private void OnTriggerEnter(Collider other)
    {
         //collect coin
        gameObject.SetActive(false);
        _currentHealth -= Random.Range(0.1f, 0.5f);
        _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
    }
}
