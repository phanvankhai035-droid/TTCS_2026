using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    [SerializeField] private float speed = 6f; 

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().ActivateShield();
            Destroy(gameObject);         
        }
    }
}