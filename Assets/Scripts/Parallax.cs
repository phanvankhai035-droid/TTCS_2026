using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Material material;
    [SerializeField] 
    private float parallaxFactor = 0.01f;
    private float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    // Update is called once per frame
    void Update()
    {
        ParallaxScroll();
    }

    private void ParallaxScroll()
    {
        float speed = GameManager.instance.GetGameSpeed() * parallaxFactor;   
        offset += Time.deltaTime * speed;
        material.SetTextureOffset("_MainTex", Vector2.right * offset);
    }
}