using UnityEngine;

public class Zapper : MonoBehaviour
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
}
