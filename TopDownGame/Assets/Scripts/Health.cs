using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public float invincibilityTime = 0.8f;
    public int Current => _currentHealth;
    
    int _currentHealth;
    bool _isInvincible;
    float _invincibleCounter;

    void Awake()
    {
        _currentHealth = maxHealth;
        Time.timeScale = 1f; // garante normal ao entrar na cena
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
    
    public void Heal(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        Debug.Log("Vida curada. Atual: " + _currentHealth);
    }
    
    public bool TakeDamage(int amount)
    {
        if (_isInvincible) return false;
        
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        _isInvincible = true;
        _invincibleCounter = invincibilityTime;

        Debug.Log("Vida atual: " + _currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
        return true;
    }

    void Die()
    {
        Debug.Log("Morreu.");
        Time.timeScale = 1f; // garante tempo normal
        SceneManager.LoadScene("MainScene"); // usa o nome exato da sua cena
    }
}