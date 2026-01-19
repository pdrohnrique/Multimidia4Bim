using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 20;
    public bool destroyOnHit;

    void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health == null) return;

        // Se for o player escondido, não causa dano
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && player.isHiding)
            return;

        bool tookDamage = health.TakeDamage(damage);

        if (tookDamage)
        {
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