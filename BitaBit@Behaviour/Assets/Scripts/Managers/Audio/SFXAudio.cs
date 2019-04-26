using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudio : PoolableItem
{
    [SerializeField]
    private AudioSource m_AudioSource;

    private float m_Counter;
    private float m_Duration;

    public void Setup(AudioClip a_Clip)
    {
        m_AudioSource.clip = a_Clip;
        m_Duration = a_Clip.length;
    }

    public void Play()
    {
        m_AudioSource.Play();
    }

    private void Update()
    {
        m_Counter += Time.deltaTime;
        if (m_Counter > m_Duration)
        {
            PoolManager.Instance.ReturnToPool(EPoolType.SFXAudioPT, gameObject);
        }
    }

}
