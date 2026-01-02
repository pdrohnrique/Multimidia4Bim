using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public float invincibilityTime = 0.8f;
    
    int _currentHealth;
    bool _isInvincible;
    float _invincibleCounter;

    void Awake()
    {
        _currentHealth = maxHealth;
    }

    void Update()
    {
        if (_isInvincible)
        {
            _invincibleCounter -= Time.deltaTime;
            if (_invincibleCounter <= 0f)
            {
                _isInvincible = false;
            }
        }
    }
    
    public bool TakeDamage(int amount)
    {
        if (_isInvincible) return false; // não tomou dano
        
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        _isInvincible = true;
        _invincibleCounter = invincibilityTime;

        Debug.Log("Vida atual: " + _currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
        return true; // dano aplicado
    }

    void Die()
    {
        Debug.Log("Morreu.");
        Time.timeScale = 0f;
        // depois: animação, reload de cena, etc.
    }
}