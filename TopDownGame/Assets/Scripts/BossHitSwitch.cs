using UnityEngine;

public class BossHitSwitch : MonoBehaviour
{
    public int damageOnUse = 25;

    BossHealth _bossHealth;
    BossSwitchSpawner _spawner;

    void Awake()
    {
        // acha o boss na cena pela tag
        GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
        if (bossObj != null)
            _bossHealth = bossObj.GetComponent<BossHealth>();

        // acha o spawner na cena
        _spawner = FindObjectOfType<BossSwitchSpawner>();
    }

    public void OnInteract()
    {
        if (_bossHealth == null) return;

        _bossHealth.TakeDamage(damageOnUse);
        Debug.Log("Dr. Silence recebeu dano da máquina!");

        if (_spawner != null)
            _spawner.OnSwitchUsed();
        else
            Destroy(gameObject);
    }
}