using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleTween
{
    public class TweenPositionOffset :  TweenTransform
    {
        [SerializeField] protected Vector3 m_maxOffset;

        [SerializeField] protected AnimationCurve m_xCurve;
        [SerializeField] protected AnimationCurve m_yCurve;
        [SerializeField] protected AnimationCurve m_zCurve;

        //--------------------------------------------------------------------------------
        public new string name { get { return "TweenPosition"; } }
        public override TweenType Type { get { return TweenType.Position; } }
        //--------------------------------------------------------------------------------
        protected override void UpdateTarget(float _pcnt)
        {
            var pos = m_target.position;
            pos.x = Mathf.LerpUnclamped(m_initialPosition.x, m_maxOffset.x, m_xCurve.Evaluate(_pcnt));
            pos.y = Mathf.LerpUnclamped(m_initialPosition.y, m_maxOffset.y, m_yCurve.Evaluate(_pcnt));
            pos.z = Mathf.LerpUnclamped(m_initialPosition.z, m_maxOffset.z, m_zCurve.Evaluate(_pcnt));
            m_target.position = pos;
        }
    }
}
