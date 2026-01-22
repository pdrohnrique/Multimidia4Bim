using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 200;
    public float invincibilityTime = 0.4f;

    public int Current => _currentHealth;

    int _currentHealth;
    bool _isInvincible;
    float _invincibleCounter;

    public System.Action OnBossDeath;

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
                _isInvincible = false;
        }
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        Debug.Log("Boss curado. HP: " + _currentHealth);
    }

    public bool TakeDamage(int amount)
    {
        if (_isInvincible) return false;

        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        _isInvincible = true;
        _invincibleCounter = invincibilityTime;

        Debug.Log("Boss tomou dano. HP: " + _currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }

        return true;
    }

    void Die()
    {
        Debug.Log("Boss morreu.");
        if (OnBossDeath != null)
            OnBossDeath.Invoke();

        // por enquanto: só desativa o boss
        gameObject.SetActive(false);
    }
}