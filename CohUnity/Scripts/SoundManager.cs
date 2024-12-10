using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    [Header("Source")]
    // [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip hurt;
    public AudioClip die;
    public AudioClip attack;
    public AudioClip respawn;

    private void Start()
    {
        // musicSource.clip = background;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void stop(){
        // musicSource.Stop();
    }
}
