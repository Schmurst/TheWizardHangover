using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleTween
{
    public class TweenRotationAbsolute : TweenTransform
    {
        [SerializeField]
        AnimationCurve m_easeCurveX;
        [SerializeField]
        AnimationCurve m_easeCurveY;
        [SerializeField]
        AnimationCurve m_easeCurveZ;

         Vector3 m_start;

        //--------------------------------------------------------------------------------
        public new string name { get { return "TweenRotation"; } }
        public override TweenType Type { get { return TweenType.Rotation; } }
        //--------------------------------------------------------------------------------
        protected override void OnAnimationInitialisation()
        {
            base.OnAnimationInitialisation();
            m_start = m_target.rotation.eulerAngles;
        }
        
        //--------------------------------------------------------------------------------
        protected override void UpdateTarget(float _pcnt)
        {
            var rot = Vector3.zero;
            rot.x = m_start.x + m_easeCurveX.Evaluate(_pcnt) * 360f;
            rot.y = m_start.y + m_easeCurveY.Evaluate(_pcnt) * 360f;
            rot.z = m_start.z + m_easeCurveZ.Evaluate(_pcnt) * 360f;
            m_target.localRotation = Quaternion.Euler(rot);
        }

        protected override void ResetTarget()
        {
            m_target.localRotation = m_initialRotation;
        }
    }
}
