using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jetpackForce = 11f; 
    [SerializeField] private float maxUpwardVelocity = 8f;
    private Rigidbody2D rb;
    private bool isThrusting = false;
    public ParticleSystem jetpackSmoke;
    private bool isGrounded;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundCheckRadius= 0.15f;
    [SerializeField]
    private LayerMask groundLayer;
    public GameObject shieldVisual; 
    private bool isShieldActive = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.freezeRotation = true; 
        
        rb.gravityScale = 1.8f; 
    }

    public void OnFly(InputAction.CallbackContext context)
    {
        if (context.started || context.performed) 
        {
            isThrusting = true;
            if (jetpackSmoke != null && !jetpackSmoke.isPlaying) jetpackSmoke.Play();
        }
        else if (context.canceled) 
        {
            isThrusting = false;
            if (jetpackSmoke != null) jetpackSmoke.Stop();
        }
    }

    void Update()
    {
        isGrounded = CheckIfGrounded();
        if (isThrusting)
        {
            rb.AddForce(Vector2.up * jetpackForce, ForceMode2D.Force);
        }
        if (rb.linearVelocity.y > maxUpwardVelocity)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxUpwardVelocity);
        }
        HandleSoundEffect();
    }
    private bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,groundCheckRadius);
    }

    private void HandleSoundEffect()
    {
        if(isGrounded && !AudioManager.instance.HasPlayEffectSound())
        {
            AudioManager.instance.PlayTapClip();
            AudioManager.instance.SetHasPlayEffectSound(true);
        }
        else if (!isGrounded)
        {
            AudioManager.instance.SetHasPlayEffectSound(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacles"))
        {
            AudioManager.instance.PlayHurtClip();
        }
        if(collision.CompareTag("coin"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.Addcoin(1);
        }
    }
    public void HitObstacle()
    {
        if (isShieldActive)
        {
            AudioManager.instance.PlayHurtClip();
            isShieldActive = false;
            shieldVisual.SetActive(false);
        }
        else
        {
            GameManager.instance.GameOver();
        }
    }
    public void ActivateShield()
    {
        isShieldActive = true;
        shieldVisual.SetActive(true);
    }
}