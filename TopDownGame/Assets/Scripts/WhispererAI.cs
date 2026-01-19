using UnityEngine;

public class WhispererAI : MonoBehaviour
{
    public Transform player;
    public float driftSpeed = 1.5f;        // movimento “fantasma” normal
    public float huntSpeed = 2f;           // quando está caçando na área
    public float chaseSpeed = 3.5f;        // perseguição direta

    public float huntRadius = 10f;         // raio grande onde ele “sente” o player
    public float chaseRadius = 3f;         // raio pequeno para perseguição

    public float driftRadius = 5f;         // raio em torno de um ponto base
    public Transform driftCenter;          // se vazio, usa a posição inicial

    enum State { Drift, HuntArea, Chase }
    State _state;

    Vector2 _currentTarget;
    PlayerController _playerController;
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (driftCenter == null)
            driftCenter = transform;

        _state = State.Drift;
        PickNewDriftTarget();

        if (player != null)
            _playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        // TRANSIÇÕES DE ESTADO
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
                if (dist > huntRadius * 1.2f) // saiu bem da área -> volta a Drift
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
                // se ficou longe demais ou lanterna apagada, volta para HuntArea
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
                if (Vector2.Distance(pos, _currentTarget) < 0.2f)
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
        Vector2 center = driftCenter.position;
        _currentTarget = center + Random.insideUnitCircle * driftRadius;
    }

    void PickNewHuntTarget()
    {
        // patrol aleatório dentro do raio grande em torno do player
        Vector2 center = player.position;
        _currentTarget = center + Random.insideUnitCircle * (huntRadius * 0.6f);
    }
    
    void OnDrawGizmosSelected()
    {
        // Desenha área de drift (em torno do driftCenter)
        Transform center = driftCenter != null ? driftCenter : transform;
        Gizmos.color = new Color(0f, 0.5f, 1f, 0.25f); // azul
        Gizmos.DrawWireSphere(center.position, driftRadius);

        // Se tiver player, desenha hunt/chase em torno dele
        if (player != null)
        {
            // Raio grande (HuntArea)
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.25f); // laranja
            Gizmos.DrawWireSphere(player.position, huntRadius);

            // Raio pequeno (Chase)
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f); // vermelho
            Gizmos.DrawWireSphere(player.position, chaseRadius);
        }

        // Ponto-alvo atual
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(_currentTarget, 0.15f);
    }

}
