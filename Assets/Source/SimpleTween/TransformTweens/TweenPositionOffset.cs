using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleTween
{
    public class TweenPositionOffset :  TweenTransform
    {
        [SerializeField] protected Vector3 m_maxOffset;
        [SerializeField] protected bool m_isLocal = true;

        [SerializeField] protected AnimationCurve m_xCurve;
        [SerializeField] protected AnimationCurve m_yCurve;
        [SerializeField] protected AnimationCurve m_zCurve;

        //--------------------------------------------------------------------------------
        public new string name { get { return "TweenPosition"; } }
        public override TweenType Type { get { return TweenType.Position; } }
        //--------------------------------------------------------------------------------
        protected override void OnAnimationInitialisation()
        {
            base.OnAnimationInitialisation();
            m_initialPosition = m_isLocal ? m_target.localPosition : m_target.position;
        }

        //--------------------------------------------------------------------------------
        protected override void UpdateTarget(float _pcnt)
        {
            var pos = m_isLocal ? m_target.localPosition : m_target.position;
            var endPos = m_isLocal ? m_initialPosition + m_maxOffset : m_maxOffset;

            pos.x = Mathf.LerpUnclamped(m_initialPosition.x, endPos.x, m_xCurve.Evaluate(_pcnt));
            pos.y = Mathf.LerpUnclamped(m_initialPosition.y, endPos.y, m_yCurve.Evaluate(_pcnt));
            pos.z = Mathf.LerpUnclamped(m_initialPosition.z, endPos.z, m_zCurve.Evaluate(_pcnt));
           
            if(m_isLocal)
                m_target.localPosition = pos;
            else
                m_target.position = pos;
        }
    }
}
