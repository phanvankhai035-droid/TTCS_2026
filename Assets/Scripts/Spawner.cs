using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject zapperPrefab;
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private GameObject warningUI;      
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private GameObject shieldpickupPrefab;

    [Header("Settings")]
    [SerializeField] private float spawnRate = 2.5f;  
    [SerializeField] private float minY = -3.3f;
    [SerializeField] private float maxY = 4f;
    [SerializeField] private float spawnX = 11f;      
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinSpawnRate = 5f;
    [SerializeField] private float shieldSpawnRate = 20f;

    private float timer = 0;
    private float coinTimer = 0;
    private float shieldTimer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            int randomType = Random.Range(0, 3);
            SpawnObstacle(randomType);
            timer = 0;
            spawnRate = Mathf.Max(0.8f, spawnRate - 0.007f);
        }
        coinTimer += Time.deltaTime;
        if (coinTimer >= coinSpawnRate)
        {
            SpawnCoinPattern();
            coinTimer = 0;
        }
        shieldTimer += Time.deltaTime;
        if (shieldTimer >= shieldSpawnRate)
        {
            SpawnShield();
            shieldTimer = 0;
        }
    }

    private void SpawnObstacle(int type)
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);

        switch (type)
        {
            case 0: 
                Instantiate(zapperPrefab, spawnPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                break;

            case 1: 
                StartCoroutine(RocketRoutine(randomY));
                break;

            case 2: 
                Instantiate(laserPrefab, new Vector3(0, randomY, 0), Quaternion.identity);
                break;
        }
    }

    IEnumerator RocketRoutine(float yPos)
    {
        if (gameCanvas != null && warningUI != null)
        {
            GameObject warning = Instantiate(warningUI, gameCanvas.transform);
            RectTransform rt = warning.GetComponent<RectTransform>();
            Vector3 worldPosition = new Vector3(spawnX, yPos, 0);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
            rt.position = new Vector3(rt.position.x, screenPos.y, 0);
        }
        yield return new WaitForSeconds(2.5f);

        Vector3 rocketPos = new Vector3(spawnX, yPos, 0);
        Instantiate(rocketPrefab, rocketPos , Quaternion.identity);
    }
    private void SpawnCoinPattern()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);
        
        int patternType = Random.Range(0, 2); 
        
        if(patternType == 0)
            SpawnLine(spawnPos, 5, 0.8f); 
        else
            SpawnBox(spawnPos, 3, 3, 0.8f);
    }
    void SpawnLine(Vector3 startPos, int count, float spacing)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = startPos + new Vector3(i * spacing, 0, 0);
            Instantiate(coinPrefab, pos, Quaternion.identity);
        }
    }
    void SpawnBox(Vector3 startPos, int width, int height, float spacing)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = startPos + new Vector3(x * spacing, y * spacing, 0);
                Instantiate(coinPrefab, pos, Quaternion.identity);
            }
        }
    }
    private void SpawnShield()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);
        Instantiate(shieldpickupPrefab, spawnPos, Quaternion.identity);
    }
}