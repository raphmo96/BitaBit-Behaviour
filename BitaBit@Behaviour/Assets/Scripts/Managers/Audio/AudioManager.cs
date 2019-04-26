using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource m_SFXAudioSource;
    [SerializeField] private AudioSource m_MusicAudioSource;
    [SerializeField] private StringAudioClipDictionary m_Tracks;
    [SerializeField] private StringAudioClipDictionary m_SFX;

    protected override void Awake()
    {
        base.Awake();
        PoolManager.Instance.CreatePool(EPoolType.SFXAudioPT, 20);
    }

    private IEnumerator SwitchMusicRoutine(float a_Duration, AudioClip a_NextClip)
    {
        float volume = m_MusicAudioSource.volume;
        
        yield return null;
        
        if (a_Duration <= 0)
        {
            yield break;
        }
        
        while (m_MusicAudioSource.volume > 0)
        {
            m_MusicAudioSource.volume -= Time.deltaTime / a_Duration;
            yield return null;
        }
        
        m_MusicAudioSource.clip = a_NextClip;
        m_MusicAudioSource.Play();

        while (m_MusicAudioSource.volume < volume)
        {
            m_MusicAudioSource.volume += Time.deltaTime / a_Duration;
            yield return null;
        }
    }

    public void SwitchMusic(float a_Duration, string a_KeyTrack)
    {
        AudioClip nextClip = m_Tracks[a_KeyTrack];

        StartCoroutine(SwitchMusicRoutine(a_Duration, nextClip));
    }

    public void PlaySFX(AudioClip a_SFX, Vector3 a_Position, Transform a_Parent = null)
    {
        SFXAudio audio = PoolManager.Instance.GetFromPool(EPoolType.SFXAudioPT, a_Position).GetComponent<SFXAudio>();
        
        if (a_Parent != null)
        {
            audio.gameObject.transform.SetParent(a_Parent);
        }
        
        audio.Setup(a_SFX);
        audio.Play();
    }

    public void PlaySFX(string a_SFX, Vector3 a_Position, Transform a_Parent = null)
    {
        SFXAudio audio = PoolManager.Instance.GetFromPool(EPoolType.SFXAudioPT, a_Position).GetComponent<SFXAudio>();
        
        if (a_Parent != null)
        {
            audio.gameObject.transform.SetParent(a_Parent);
        }
        
        audio.Setup(m_SFX[a_SFX]);
        audio.Play();
    }

    public void PlayAudibleCue(AudioClip a_SFX)
    {
        m_SFXAudioSource.clip = a_SFX;
        m_SFXAudioSource.Play();
    }
}


