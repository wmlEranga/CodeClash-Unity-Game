using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("-------Audio Source-------")]
    [SerializeField] AudioSource musicSource;


    [Header("-------Audio Clip-------")]
    public AudioClip menuSound;
    public AudioClip creditSound;

    private void Start()
    {
        musicSource.clip = menuSound;
        musicSource.Play();
    }

    public void playSound(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

}
