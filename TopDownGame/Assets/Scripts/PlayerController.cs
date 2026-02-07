using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float runSpeed = 8f;
    
    [Header("Animator")]
    public Animator animator;
    
    [Header("Lanterna")]
    public Light2D flashlight;
    public float maxBattery = 100f;
    public float batteryDrain = 10f;
    public float BatteryPercent => (maxBattery <= 0f) ? 0f : (_battery / maxBattery * 100f);
    
    [HideInInspector] public bool canMove = true;
    
    Vector2 _input;
    private float _battery;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _animator;
    public bool hasKey;
    public bool isHiding;
    public Inventory inventory;
    public Health health;
    public bool IsFlashlightOn => flashlight != null && flashlight.enabled;
    
    public void GiveKey()
    {
        hasKey = true;
        Debug.Log("Pegou a chave!");
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        
        _battery = maxBattery;
        flashlight.enabled = false;
        
        if (inventory == null)
            inventory =  GetComponent<Inventory>();
        if (health == null)
            health = GetComponent<Health>();
    }
    
    void Update()
    {
        // Primeiro trata entrada (E), mesmo se estiver escondido
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction(); // função que já faz porta / esconderijo
            //animator.SetTrigger("Attack");
        }
        
        //if (Input.GetKeyDown(KeyCode.Space)) animator.SetTrigger("Jump");
        
        // Se estiver escondido, não processa movimento e lanterna
        if (isHiding)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }
        
        // MOVIMENTO TOP-DOWN (WASD) - só lê input aqui
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (_animator != null)
        {
            _animator.SetFloat("InputX", horizontal);
            _animator.SetFloat("InputY", vertical);
    
            if (_input.magnitude > 0.1f && (horizontal != 0 || vertical != 0))  // Movendo?
            {
                _animator.SetBool("isWalking", true);
                _animator.SetFloat("LastInputX", horizontal);  // SALVA DURANTE movimento
                _animator.SetFloat("LastInputY", vertical);
            }
            else
            {
                _animator.SetBool("isWalking", false);
                // NÃO sobrescreve LastInput quando parado!
            }
        }

        
        _input = new Vector2(horizontal, vertical).normalized;
        
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
        
        // 1 = usar medicamento
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (inventory != null && inventory.UseMed())
            {
                if (health != null)
                    health.Heal(30); // ajusta esse valor depois
            }
        }

        // 2 = usar pilha
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (inventory != null && inventory.UseBattery())
            {
                RechargeBattery(25f); // ajusta valor depois
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
        
        foreach (var h in hits)
        {
            ItemPickup pickup = h.GetComponent<ItemPickup>();
            if (pickup != null)
            {
                pickup.TryPickup();
                return;
            }
        }
        
        foreach (var h in hits)
        {
            BossHitSwitch bossSwitch = h.GetComponent<BossHitSwitch>();
            if (bossSwitch != null)
            {
                bossSwitch.OnInteract();
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

    private void RechargeBattery(float amount)
    {
        _battery += amount;
        _battery = Mathf.Clamp(_battery, 0f, maxBattery);
        
        // Se quiser, liga a lanterna se tiver bateria de novo:
        // if (_battery > 0f && !flashlight.enabled)
        // flashlight.enabled = true;
    }
}