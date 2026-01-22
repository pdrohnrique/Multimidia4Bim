using UnityEngine;

public class BossHitSwitch : MonoBehaviour
{
    public Health bossHealth;
    public int damageOnUse = 25;

    public void OnInteract()
    {
        if (bossHealth == null) return;

        bossHealth.TakeDamage(damageOnUse);
        Debug.Log("Dr. Silence recebeu dano da máquina!");
    }
}