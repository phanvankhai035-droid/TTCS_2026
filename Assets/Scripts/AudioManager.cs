using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField]
    private AudioSource effectSource;
    [SerializeField]
    private AudioClip flyClip;
    [SerializeField]
    private AudioClip tapClip;
    [SerializeField]
    private AudioClip hurtClip;

    private bool hasPlayEffectSound = false;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public bool HasPlayEffectSound()
    {
        return hasPlayEffectSound;
    }
    public void SetHasPlayEffectSound(bool value)
    {
        hasPlayEffectSound=value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectSource.Stop();
        hasPlayEffectSound=true;
    }
    public void PlayFlyClip()
    {
        effectSource.PlayOneShot(flyClip);
    }
    public void PlayTapClip()
    {
        effectSource.PlayOneShot(tapClip);
    }
    public void PlayHurtClip()
    {
        effectSource.PlayOneShot(hurtClip);
    }
}
