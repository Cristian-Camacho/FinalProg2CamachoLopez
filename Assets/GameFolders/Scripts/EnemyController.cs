using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Transform> spawnPos;
    public GameObject enemyPrefab;
    private int enemysAlive;
    private int generateAmount = 10;
    public static EnemyController instance;

    // Use this for initialization
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < generateAmount; i++)
        {
            int spawnID = Random.Range(0, spawnPos.Count - 1);
            var enemy = Instantiate(enemyPrefab, spawnPos[spawnID].position, Quaternion.identity);
            enemysAlive++;

        }
    }

    public void MultiplyEnemies()
    {
        enemysAlive--;
        if (enemysAlive == 0)
            GameController.instance.EnterConstrucctionStage();

        int randomChance = Random.Range(0, 100);
        if (randomChance % 10 == 0)
            generateAmount++;

    }
}
