using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().HitObstacle();
        }
    }
}