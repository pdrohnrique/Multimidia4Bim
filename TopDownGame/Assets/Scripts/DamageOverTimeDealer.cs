using UnityEngine;

public class DamageOverTimeDealer : MonoBehaviour
{
    public int damage = 10;              // dano por tick
    public float damageInterval = 0.5f;  // intervalo entre danos em segundos

    Collider2D _targetInside;
    float _timer;

    void OnTriggerEnter2D(Collider2D other)
    {
        _targetInside = other;
        _timer = 0f; // permite dano imediato
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == _targetInside)
        {
            _targetInside = null;
        }
    }

    void Update()
    {
        if (_targetInside == null) return;

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            ApplyDamage(_targetInside);
            _timer = damageInterval;
        }
    }

    void ApplyDamage(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health == null) return;

        bool tookDamage = health.TakeDamage(damage);
        if (!tookDamage) return; // ainda invencível

        PlayerDamageFeedback feedback = other.GetComponent<PlayerDamageFeedback>();
        if (feedback != null)
        {
            Vector2 dir = (other.transform.position - transform.position);
            feedback.OnDamaged(dir);
        }
    }
}