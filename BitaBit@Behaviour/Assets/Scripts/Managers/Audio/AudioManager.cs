using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_Instance;
    public static AudioManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    [SerializeField]
    private AudioSource m_AudioSourceMusic;

    [SerializeField]
    private GameObject m_SFX;

    [SerializeField]
    private AudioClip m_MainMenuMusic;
    [SerializeField]
    private AudioClip m_GameMusic;


    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(string aAudioSource)
    {
        if(aAudioSource == "MainMenu")
        {
            if(aAudioSource != null)
            {
                m_AudioSourceMusic.Stop();
            }
            m_AudioSourceMusic.clip = m_MainMenuMusic;
            m_AudioSourceMusic.Play();
        }
    }

    public void StopMusic()
    {
        if (m_AudioSourceMusic != null)
        {
            m_AudioSourceMusic.Stop();
        }
    }

    public void PlaySFX(AudioClip aClip, Vector3 aPosition)
    {
        GameObject audio = Instantiate(m_SFX, aPosition, new Quaternion());
        audio.GetComponent<SFXAudio>().Setup(aClip);
        audio.GetComponent<SFXAudio>().Play();
    }
}


