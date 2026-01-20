using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;
    public Transform firePoint;          // vazio na frente do boss
    public float timeBetweenShots = 1.2f;
    public int burstCount = 3;           // quantos tiros por rajada
    public float burstInterval = 0.2f;   // intervalo entre tiros da rajada

    float _shotTimer;

    void Update()
    {
        if (player == null || projectilePrefab == null || firePoint == null) return;

        // olha pro player (opcional, se tiver animação que rotaciona)
        Vector2 dir = (player.position - transform.position).normalized;
        // aqui você pode usar dir pra flipar sprite, etc.

        _shotTimer -= Time.deltaTime;
        if (_shotTimer <= 0f)
        {
            _shotTimer = timeBetweenShots;
            StartCoroutine(ShootBurst());
        }
    }

    System.Collections.IEnumerator ShootBurst()
    {
        for (int i = 0; i < burstCount; i++)
        {
            ShootAtPlayer();
            if (i < burstCount - 1)
                yield return new WaitForSeconds(burstInterval);
        }
    }

    void ShootAtPlayer()
    {
        if (player == null) return;

        Vector2 dir = (player.position - firePoint.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        BossProjectile bp = proj.GetComponent<BossProjectile>();
        if (bp != null)
            bp.Init(dir);
    }
}