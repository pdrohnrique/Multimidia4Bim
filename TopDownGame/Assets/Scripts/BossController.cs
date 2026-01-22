using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;

    [Header("Movimento")]
    public BoxCollider2D moveArea;
    public float baseDriftSpeed = 1.5f;
    public float phase2SpeedMultiplier = 1.4f;
    public float phase3SpeedMultiplier = 1.8f;
    public float minTargetDistance = 0.3f;

    [Header("Ataque - Projétil simples")]
    public GameObject projectilePrefab;
    public float phase1ShotInterval = 1.2f;
    public float phase2ShotInterval = 1.0f;
    public float phase3ShotInterval = 0.7f;

    public Transform firePoint;

    [Header("Fases (percentual de vida)")]
    [Range(0.1f, 0.9f)] public float phase2Threshold = 0.66f;
    [Range(0.1f, 0.9f)] public float phase3Threshold = 0.33f;

    enum Phase { Phase1, Phase2, Phase3 }
    Phase _phase;

    Rigidbody2D _rb;
    BossHealth _health;
    Vector2 _currentTarget;

    float _shotTimer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _health = GetComponent<BossHealth>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Start()
    {
        _phase = Phase.Phase1;
        PickNewMoveTarget();
        ResetShotTimer();
    }

    void Update()
    {
        if (player == null || _health == null) return;

        UpdatePhase();
        HandleShooting(Time.deltaTime);
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void UpdatePhase()
    {
        float healthPercent = (float)_health.Current / _health.maxHealth;

        if (healthPercent <= phase3Threshold)
            _phase = Phase.Phase3;
        else if (healthPercent <= phase2Threshold)
            _phase = Phase.Phase2;
        else
            _phase = Phase.Phase1;
    }

    void HandleMovement()
    {
        if (moveArea == null || _rb == null) return;

        Vector2 pos = transform.position;
        Vector2 target = _currentTarget;
        float speed = baseDriftSpeed;

        switch (_phase)
        {
            case Phase.Phase1:
                speed = baseDriftSpeed;
                break;

            case Phase.Phase2:
                speed = baseDriftSpeed * phase2SpeedMultiplier;
                break;

            case Phase.Phase3:
                speed = baseDriftSpeed * phase3SpeedMultiplier;
                Vector2 towardPlayer = player.position;
                target = Vector2.Lerp(_currentTarget, towardPlayer, 0.4f);
                break;
        }

        if (Vector2.Distance(pos, _currentTarget) < minTargetDistance)
            PickNewMoveTarget();

        Vector2 dir = (target - pos).normalized;
        _rb.linearVelocity = dir * speed;
    }

    void HandleShooting(float dt)
    {
        if (firePoint == null || projectilePrefab == null) return;

        _shotTimer -= dt;

        float interval = phase1ShotInterval;
        if (_phase == Phase.Phase2) interval = phase2ShotInterval;
        else if (_phase == Phase.Phase3) interval = phase3ShotInterval;

        if (_shotTimer <= 0f)
        {
            _shotTimer = interval;
            ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
        if (player == null) return;

        Vector2 dir = (player.position - firePoint.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        BossProjectile bp = proj.GetComponent<BossProjectile>();
        if (bp != null)
        {
            bp.Init(dir);
        }
    }

    void PickNewMoveTarget()
    {
        if (moveArea == null) return;

        Bounds b = moveArea.bounds;
        float x = Random.Range(b.min.x, b.max.x);
        float y = Random.Range(b.min.y, b.max.y);
        _currentTarget = new Vector2(x, y);
    }

    void ResetShotTimer()
    {
        _shotTimer = phase1ShotInterval;
    }

    void OnDrawGizmosSelected()
    {
        if (moveArea != null)
        {
            Gizmos.color = new Color(0.2f, 0.8f, 0.2f, 0.25f);
            Gizmos.DrawWireCube(moveArea.bounds.center, moveArea.bounds.size);
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(_currentTarget, 0.15f);
    }
}