using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleTween
{
    public class EventStateTweener : EventTweener
    {

        protected override void Execute(EventType _type)
        {
            if (!ShouldPlayEvent(_type))
                return;

            if (!m_isTweenInProgress)
            {
                Play();
                m_timeOfLastEvent = Time.time;
            }
            else
            {
                m_isTweenInProgress = false;
            }    
        }

        protected override IEnumerator Co_PlayTweens(Action _onComplete = null)
        {
            Debug.LogFormat("Started Tween on {0}", name);
            m_isTweenInProgress = true;

            START:

            var animations = new List<Coroutine>();
            int started = 0, completed = 0;
            Action onCompleted = () => { ++completed; };
            var waitForAnimsToFinish = new WaitUntil(() => { return started == completed || !m_isTweenInProgress; });

            for (int i = 0; i < m_tweens.Count; i++)
            {
                var routine = StartCoroutine(m_tweens[i].Co_PlayTween(onCompleted));
                animations.Add(routine);
                started++;
            }

            yield return waitForAnimsToFinish;
            if (m_isTweenInProgress)
                goto START;

            if (_onComplete != null)
                _onComplete();

            m_isTweenInProgress = false;
        }
    }
}