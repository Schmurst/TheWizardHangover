using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleTween
{
    public class EventToggleTweener : EventTweener
    {
        [SerializeField] List<Tween> m_returnTweens;
        bool m_isActive;

        protected override void Execute(EventType _type)
        {
            if (ShouldPlayEvent(_type))
            {
                Play();
                m_isActive = !m_isActive;
                m_timeOfLastEvent = Time.time;
            }
        }

        protected override bool ShouldPlayEvent(EventType _type)
        {
            if (m_isTweenInProgress)
                return false;

            return base.ShouldPlayEvent(_type);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (m_target == null)
                return;

            var type = m_target.GetType();
            HashSet<TweenType> validTweenTypes;
            if (!TweenManager.UsableTweensByType.TryGetValue(type, out validTweenTypes))
            {
                Debug.LogFormat("No TweenTypes for {0}", m_target.GetType());
                m_target = null;
                return;
            }

            for (int i = m_returnTweens.Count - 1; i >= 0; i--)
            {
                if (!validTweenTypes.Contains(m_returnTweens[i].Type))
                    m_returnTweens.RemoveAt(i);
                else
                    m_returnTweens[i].Initialise(m_target);
            }
        }

        protected override IEnumerator Co_PlayTweens(Action _onComplete = null)
        {
            Debug.LogFormat("Started Tween on {0}", name);
            m_isTweenInProgress = true;

            var animations = new List<Coroutine>();
            int started = 0, completed = 0;
            Action onCompleted = () => { ++completed; };
            var waitForAnimsToFinish = new WaitUntil(() => { return started == completed; });
            var tweens = m_isActive ? m_returnTweens : m_tweens;

            for (int i = 0; i < m_tweens.Count; i++)
            {
                var routine = StartCoroutine(tweens[i].Co_PlayTween(onCompleted));
                animations.Add(routine);
                started++;
            }

            yield return waitForAnimsToFinish;

            if (_onComplete != null)
                _onComplete();

            m_isTweenInProgress = false;
        }
    }
}
