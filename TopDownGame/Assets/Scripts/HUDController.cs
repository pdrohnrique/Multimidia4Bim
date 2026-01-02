using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Refs")]
    public PlayerController player;
    public Health health;
    public Inventory inventory;

    [Header("Vida")]
    public Image hpFillImage;
    public TextMeshProUGUI hpText;

    [Header("Bateria")]
    public Image batteryFillImage;
    public TextMeshProUGUI batteryText;

    [Header("Inventário")]
    public TextMeshProUGUI medCountText;
    public TextMeshProUGUI batteryCountText;
    public TextMeshProUGUI noteCountText;
    public TextMeshProUGUI keyCountText;

    void Start()
    {
        if (player == null)
            player = FindFirstObjectByType<PlayerController>();
        if (health == null && player != null)
            health = player.GetComponent<Health>();
        if (inventory == null && player != null)
            inventory = player.GetComponent<Inventory>();
    }

    void Update()
    {
        if (player == null || health == null || inventory == null)
            return;

        // Vida
        float hpPercent = (float)health.Current / health.maxHealth;
        hpFillImage.fillAmount = hpPercent;
        hpText.text = $"{health.Current}/{health.maxHealth}";

        // Bateria
        float batPercent01 = player.BatteryPercent / 100f;
        batteryFillImage.fillAmount = batPercent01;
        batteryText.text = $"{Mathf.RoundToInt(player.BatteryPercent)}%";

        // Inventário (contadores)
        medCountText.text = inventory.medsCount.ToString();
        batteryCountText.text = inventory.batteryCount.ToString();
        noteCountText.text = inventory.notesCount.ToString();
        keyCountText.text = inventory.keysCount.ToString();
    }
}