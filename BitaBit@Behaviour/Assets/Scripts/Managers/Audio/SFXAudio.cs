using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXAudio : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private float m_Counter = 0f;
    private float m_Duration = 3f;

    private void Awake()
    {
        m_AudioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        m_Counter += Time.deltaTime;
        if (m_Counter > m_Duration)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(AudioClip i_Clip)
    {
        m_AudioSource.clip = i_Clip;
        m_Duration = i_Clip.length;
    }

    public void Play()
    {
        m_AudioSource.Play();
    }
}
