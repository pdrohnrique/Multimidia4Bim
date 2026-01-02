using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 20;
    public bool destroyOnHit;

    void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            // Tenta aplicar dano
            bool tookDamage = health.TakeDamage(damage);

            if (tookDamage)
            {
                // Se tomou dano de fato, aplica feedback
                PlayerDamageFeedback feedback = other.GetComponent<PlayerDamageFeedback>();
                if (feedback != null)
                {
                    Vector2 dir = (other.transform.position - transform.position);
                    feedback.OnDamaged(dir);
                }

                if (destroyOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}