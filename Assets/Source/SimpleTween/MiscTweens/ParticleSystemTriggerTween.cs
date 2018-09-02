using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemTriggerTween : MonoBehaviour, SimpleTween.ITweenEvent
{
    [SerializeField] ParticleSystem m_particles;
    [SerializeField] float m_duration = 1f;
    [SerializeField] float m_delay = 0f;

    void Awake()
    {
        m_particles.Pause();
    }

    void SimpleTween.ITweenEvent.Play()
    {
        DoAfterSeconds(m_particles.Play, m_delay);
        DoAfterSeconds(m_particles.Stop, m_delay+m_duration);
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