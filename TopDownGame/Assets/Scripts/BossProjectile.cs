using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 6f;
    public float lifetime = 4f;

    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 direction)
    {
        if (_rb != null)
            _rb.linearVelocity = direction.normalized * speed;

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // só reage se for o player
        if (!other.CompareTag("Player"))
            return;

        // aqui você põe o dano no player, se quiser
        // var health = other.GetComponent<Health>();
        // if (health != null) health.TakeDamage(damageAmount);

        Destroy(gameObject);
    }

}