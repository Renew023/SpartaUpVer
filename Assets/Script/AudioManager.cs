using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    // Start is called before the first frame update

    AudioSource audio;
    public AudioClip Startclip;
    public AudioClip Gameclip;
    public AudioClip Feverclip;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = Startclip;
        audio.Play();
    }

    public void musicChange()
    {
        audio.clip = Gameclip;
        audio.Play();
    }
    public void FeverChange()
    {
        audio.clip = Feverclip;
        audio.Play();
    }
}
