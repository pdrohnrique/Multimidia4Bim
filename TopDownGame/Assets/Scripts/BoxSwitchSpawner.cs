using UnityEngine;

public class BossSwitchSpawner : MonoBehaviour
{
    public BoxCollider2D spawnArea;
    public GameObject switchPrefab;
    public float respawnDelay = 3f;

    GameObject _currentSwitch;

    void Start()
    {
        SpawnNewSwitch();
    }

    public void OnSwitchUsed()
    {
        if (_currentSwitch != null)
            Destroy(_currentSwitch);

        Invoke(nameof(SpawnNewSwitch), respawnDelay);
    }

    void SpawnNewSwitch()
    {
        if (spawnArea == null || switchPrefab == null) return;

        Bounds b = spawnArea.bounds;
        float x = Random.Range(b.min.x, b.max.x);
        float y = Random.Range(b.min.y, b.max.y);
        Vector2 pos = new Vector2(x, y);

        _currentSwitch = Instantiate(switchPrefab, pos, Quaternion.identity);
    }
}