using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public static Dictionary<string, AudioClip> m_audioClips = null;

    private AudioSource m_sfx;

    void Awake()
    {
        if (m_audioClips == null)
            m_audioClips = Resources
                .LoadAll("Audio", typeof(AudioClip))
                .OfType<AudioClip>()
                .ToDictionary(o => o.name, o => o);

        m_sfx = this.GetComponent<AudioSource>();
    }

    public void Play(string clip)
    {
        if (!m_audioClips.ContainsKey(clip))
        {
            Debug.LogErrorFormat("Cannot find sound {0}", clip);
        }
        else
        {
            m_sfx.clip = m_audioClips[clip];
            m_sfx.Play();
        }
    }
}
