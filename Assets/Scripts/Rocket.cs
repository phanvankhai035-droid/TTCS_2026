using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float speed = 15f; 

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}