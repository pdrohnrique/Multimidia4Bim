using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float runSpeed = 8f;
    
    [Header("Lanterna")]
    public Light2D flashlight;
    public float maxBattery = 100f;
    public float batteryDrain = 10f;
    
    [HideInInspector] public bool canMove = true;
    
    private float _battery;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    public bool hasKey;
    public bool isHiding;
    Vector2 _input;
    
    public void GiveKey()
    {
        hasKey = true;
        Debug.Log("Pegou a chave!");
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _battery = maxBattery;
        flashlight.enabled = false;
    }
    
    void Update()
    {
        // Primeiro trata entrada (E), mesmo se estiver escondido
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction(); // função que já faz porta / esconderijo
        }
        
        // Se estiver escondido, não processa movimento e lanterna
        if (isHiding)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }
        
        // MOVIMENTO TOP-DOWN (WASD) - só lê input aqui
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _input = new Vector2(horizontal, vertical).normalized;
        
        // FLIP SPRITE (olha para onde anda)
        if (horizontal > 0) _sr.flipX = false;
        else if (horizontal < 0) _sr.flipX = true;
        
        // LANTERNA (F)
        if (Input.GetKeyUp(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }
        
        // DRAIN BATTERY
        if (flashlight.enabled)
        {
            _battery -= batteryDrain * Time.deltaTime;
            if (_battery <= 0f)
            {
                flashlight.enabled = false;
                _battery = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        
        // CORRER (SHIFT)
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;
        _rb.linearVelocity = _input * currentSpeed;
    }
    
    void HandleInteraction()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        
        // 1) Checa porta
        foreach (var h in hits)
        {
            Door door = h.GetComponent<Door>();
            if (door != null)
            {
                door.TryUseDoor();
                return;
            }
        }
        
        // 2) Checa esconderijo
        foreach (var h in hits)
        {
            HidingSpot hide = h.GetComponent<HidingSpot>();
            if (hide != null)
            {
                hide.TryToggleHide();
                return;
            }
        }
    }

    public void ToggleHide()
    {
        isHiding = !isHiding;

        if (isHiding)
        {
            _rb.linearVelocity = Vector2.zero; // para movimento
            _sr.enabled = false; // desativa sprite
            
            // Apagar lanterna ao se esconder
            if (flashlight != null && flashlight.enabled)
                flashlight.enabled = false;
        }
        else
        {
            _sr.enabled = true;
        }
    }
}