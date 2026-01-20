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
        _rb.linearVelocity = direction.normalized * speed;
        Destroy(gameObject, lifetime);
    }
}