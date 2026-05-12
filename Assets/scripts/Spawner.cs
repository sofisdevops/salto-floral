using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float timeBetweenSpawns = 1.2f;

    void Start()
    {
        InvokeRepeating("Spawn", 0f, timeBetweenSpawns);
    }

    void Spawn()
    {
        float randomValue = Random.Range(0f, 100f);
        int index = 0;

        if (randomValue < 60f)
        {
            index = 0;
        }
        else if (randomValue < 90f)
        {
            index = 1;
        }
        else
        {
            index = 2;
        }

        float randomX = Random.Range(-7f, 7f);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0);
        Instantiate(prefabs[index], spawnPos, Quaternion.identity);
    }
}