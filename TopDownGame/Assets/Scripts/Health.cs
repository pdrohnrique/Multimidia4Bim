using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public float invincibilityTime = 0.8f;
    public int Current => currentHealth;
    
    [FormerlySerializedAs("_currentHealth")] public int currentHealth;
    bool _isInvincible;
    float _invincibleCounter;

    void Awake()
    {
        currentHealth = maxHealth;
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
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Vida curada. Atual: " + currentHealth);
    }
    
    public bool TakeDamage(int amount)
    {
        if (_isInvincible) return false;
        
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        _isInvincible = true;
        _invincibleCounter = invincibilityTime;

        Debug.Log("Vida atual: " + currentHealth);

        if (currentHealth <= 0)
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