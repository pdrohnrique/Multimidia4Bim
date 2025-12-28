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
    
    private float _battery;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    public bool hasKey;

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
        // MOVIMENTO TOP-DOWN (WASD)
        float horizontal = Input.GetAxisRaw("Horizontal");  // A/D ou setas
        float vertical = Input.GetAxisRaw("Vertical");    // W/S ou setas
        
        Vector2 move = new Vector2(horizontal, vertical).normalized;
        
        // CORRER (Shift)
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;
        _rb.linearVelocity = move * currentSpeed;
        
        // FLIP SPRITE (olha para onde anda)
        if (horizontal > 0) _sr.flipX = false;
        else if (horizontal < 0) _sr.flipX = true;
        
        // INTERAGIR (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);

            foreach (var h in hits)
            {
                Door door = h.GetComponent<Door>();

                if (door != null)
                {
                    door.TryUseDoor();
                    break;
                }
            }
        }
        
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
}