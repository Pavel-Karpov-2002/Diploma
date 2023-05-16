using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioParameters audioParameters;

    private void Start()
    {
        audioSource.clip = (audioParameters.Movement);
    }
    
    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    public void SetAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayOneAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip);
    }
}
