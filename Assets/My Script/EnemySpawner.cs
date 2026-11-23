using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 敌人预制体数组
    public float spawnInterval = 5f; // 敌人生成间隔
    public int maxEnemies = 5; // 最大敌人数量
    private int currentEnemies = 0;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (currentEnemies < maxEnemies)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            Vector3 randomPosition = RandomPosition();
            GameObject enemy = Instantiate(enemyPrefabs[randomIndex], randomPosition, Quaternion.identity);
            enemy.GetComponent<EnemyController>().OnDeath += OnEnemyDeath; // 假设敌人死亡时会调用OnDeath事件
            currentEnemies++;
        }
    }

    Vector3 RandomPosition()
    {
        // 在地图范围内随机生成位置
        float x = Random.Range(-10f, 10f); // 假设地图范围为-10到10
        float z = Random.Range(-10f, 10f);
        return new Vector3(x, 0, z);
    }

    void OnEnemyDeath()
    {
        currentEnemies--;
    }
}