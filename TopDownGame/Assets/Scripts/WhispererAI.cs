using UnityEngine;

public class WhispererAI : MonoBehaviour
{
    public Transform player;
    public float driftSpeed = 1.5f;
    public float huntSpeed = 2f;
    public float chaseSpeed = 3.5f;

    public float huntRadius = 10f;
    public float chaseRadius = 3f;

    [Header("Drift Area")]
    public BoxCollider2D driftArea;   // NOVO: área retangular
    public float minDriftPointDistance = 0.2f;

    enum State { Drift, HuntArea, Chase }
    State _state;

    Vector2 _currentTarget;
    PlayerController _playerController;
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _state = State.Drift;
        PickNewDriftTarget();

        if (player != null)
            _playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        switch (_state)
        {
            case State.Drift:
                if (dist <= huntRadius)
                {
                    _state = State.HuntArea;
                    PickNewHuntTarget();
                }
                break;

            case State.HuntArea:
                if (dist > huntRadius * 1.2f)
                {
                    _state = State.Drift;
                    PickNewDriftTarget();
                }
                else if (dist <= chaseRadius && _playerController != null && _playerController.IsFlashlightOn)
                {
                    _state = State.Chase;
                }
                break;

            case State.Chase:
                if (dist > chaseRadius * 1.5f || (_playerController != null && !_playerController.IsFlashlightOn))
                {
                    _state = dist <= huntRadius ? State.HuntArea : State.Drift;
                    if (_state == State.HuntArea) PickNewHuntTarget();
                    else PickNewDriftTarget();
                }
                break;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 pos = transform.position;
        Vector2 target = pos;
        float speed = driftSpeed;

        switch (_state)
        {
            case State.Drift:
                target = _currentTarget;
                speed = driftSpeed;
                if (Vector2.Distance(pos, _currentTarget) < minDriftPointDistance)
                    PickNewDriftTarget();
                break;

            case State.HuntArea:
                target = _currentTarget;
                speed = huntSpeed;
                if (Vector2.Distance(pos, _currentTarget) < 0.3f)
                    PickNewHuntTarget();
                break;

            case State.Chase:
                target = player.position;
                speed = chaseSpeed;
                break;
        }

        Vector2 dir = (target - pos).normalized;
        _rb.linearVelocity = dir * speed;
    }

    void PickNewDriftTarget()
    {
        if (driftArea == null)
        {
            _currentTarget = transform.position;
            return;
        }

        Bounds b = driftArea.bounds;
        float x = Random.Range(b.min.x, b.max.x);
        float y = Random.Range(b.min.y, b.max.y);
        _currentTarget = new Vector2(x, y);
    }

    void PickNewHuntTarget()
    {
        Vector2 center = player.position;
        _currentTarget = center + Random.insideUnitCircle * (huntRadius * 0.6f);
    }

    void OnDrawGizmosSelected()
    {
        // Drift area
        if (driftArea != null)
        {
            Gizmos.color = new Color(0f, 0.5f, 1f, 0.25f);
            Gizmos.DrawWireCube(driftArea.bounds.center, driftArea.bounds.size);
        }

        if (player != null)
        {
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.25f);
            Gizmos.DrawWireSphere(player.position, huntRadius);

            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            Gizmos.DrawWireSphere(player.position, chaseRadius);
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(_currentTarget, 0.15f);
    }
}
