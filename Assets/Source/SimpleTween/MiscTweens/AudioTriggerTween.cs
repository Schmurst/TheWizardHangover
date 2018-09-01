using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerTween : MonoBehaviour, SimpleTween.ITweenEvent
{
    [SerializeField] AudioInfo m_audioInfo;

    public void Play()
    {
        AudioManager.Instance.Stop(m_audioInfo.m_Name);
        AudioManager.Instance.PlayAudio(m_audioInfo);
    }
}