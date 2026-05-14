using UnityEngine;

public class Coin : MonoBehaviour
{
    public float leftBoundary = -10f;
    // Update is called once per frame
    void Update()
    {
        MoverObstacle();
    }
    private void MoverObstacle()
    {
        transform.position += Vector3.left * GameManager.instance.GetGameSpeed() * Time.deltaTime;

        if (transform.position.x < leftBoundary)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
