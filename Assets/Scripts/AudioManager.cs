using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSourse;
    [SerializeField] AudioSource SFXSourse;

    [Header("Auido Clip")]
    public AudioClip background;
    public AudioClip hurt;
    public AudioClip jump;
    public AudioClip checkpoint;
    public AudioClip getcoin;
    public AudioClip death;

    private void Start()
    {
        musicSourse.clip = background;
        musicSourse.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSourse.PlayOneShot(clip);
    }
}
