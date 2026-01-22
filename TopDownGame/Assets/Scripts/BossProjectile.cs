using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 6f;
    public float lifetime = 4f;
    public bool stickOnHit;          // se for poça, não destrói na primeira colisão
    public float stickLifetime = 3f; // tempo extra na arena

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
        // aqui o DamageDealer do projétil já cuida de dar dano no player
        if (!stickOnHit)
        {
            Destroy(gameObject);
        }
        else
        {
            // vira "poça": para de se mexer e fica um tempo
            if (_rb != null)
                _rb.linearVelocity = Vector2.zero;

            Destroy(gameObject, stickLifetime);
        }
    }
}