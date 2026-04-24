using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Transform highPos;
    [SerializeField] private Transform midPos;
    [SerializeField] private Transform lowPos;
    
    private float timer = 0;
    [SerializeField] private float spawnRate = 2f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    private void SpawnObstacle()
    {
        int index = Random.Range(0, 3);

        if (index == 0) 
        {
            Instantiate(obstacles[index], lowPos.position, obstacles[index].transform.rotation);
        }
        else if (index == 1)
        {
            Instantiate(obstacles[index], midPos.position, obstacles[index].transform.rotation);
        }
        else
        {
            Instantiate(obstacles[index], highPos.position, obstacles[index].transform.rotation);
        }
    }
}