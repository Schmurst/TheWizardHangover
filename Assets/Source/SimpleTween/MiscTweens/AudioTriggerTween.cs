using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerTween : MonoBehaviour, SimpleTween.ITweenEvent
{
    [SerializeField] AudioInfo m_audioInfo;
    [SerializeField] float m_delay = 0;

    public void Play()
    {
        DoAfterSeconds(()=>{
            AudioManager.Instance.Stop(m_audioInfo.m_Name);
            AudioManager.Instance.PlayAudio(m_audioInfo);
        }, m_delay);

    }

    IEnumerator DoAfterSecondsRoutine(System.Action _action, float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        _action();
    }

    void DoAfterSeconds(System.Action _action, float _seconds)
    {
        StartCoroutine(DoAfterSecondsRoutine(_action, _seconds));
    }
}