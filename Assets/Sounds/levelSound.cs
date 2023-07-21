using UnityEngine;

public class levelSound : MonoBehaviour
{
    [Header("-------Audio Source-------")]
    [SerializeField] private AudioSource musicSource;

    [Header("-------Audio Clip-------")]
    public AudioClip Level;

    public static levelSound instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = Level;
        musicSource.Play();
    }

    public AudioSource MusicSource
    {
        get { return musicSource; }
    }

    public void playSound(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }
}