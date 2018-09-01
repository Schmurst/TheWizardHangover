using System;
using System.Collections;
using System.Collections.Generic;
using SimpleTween;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Getting local position from inside the current object
/// </summary>
public class SplineController : MonoBehaviour, SimpleTween.ITweenEvent// IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
                                //IPointerUpHandler, IPointerClickHandler
{
    [SerializeField]
    SimpleTween.EventType m_eventType;
    [SerializeField]
    float m_Duration;
    [SerializeField]
    private Transform m_TransformRoot;
    [SerializeField]
    private List<Vector3> m_Positions =  new List<Vector3>();
    [SerializeField]
    private LeanTweenType m_LeanType;
    [SerializeField]
    AnimationCurve m_AnimCurve;

    private LTSpline m_splineVisualizer;
    private bool isExecuting = false;

    //void OnEnable() { if (m_eventType == SimpleTween.EventType.onEnable) ExecuteSpline(); }
    //public void OnPointerEnter(PointerEventData eventData) { if (m_eventType == SimpleTween.EventType.pointerEnter && !isExecuting) StartCoroutine(ExecuteSpline()); }
    //public void OnPointerExit(PointerEventData eventData) { if (m_eventType == SimpleTween.EventType.pointerExit && !isExecuting) StartCoroutine(ExecuteSpline()); }
    //public void OnPointerDown(PointerEventData eventData) { if (m_eventType == SimpleTween.EventType.pointerDown && !isExecuting) StartCoroutine(ExecuteSpline()); }
    //public void OnPointerUp(PointerEventData eventData) { if (m_eventType == SimpleTween.EventType.pointerUp && !isExecuting) StartCoroutine(ExecuteSpline()); }
    //public void OnPointerClick(PointerEventData eventData) {
    //    if (m_eventType == SimpleTween.EventType.pointerClick && !isExecuting)
    //        StartCoroutine(ExecuteSpline());
    //}

    // Use this for initialization
    void Start () {

        if (m_TransformRoot == null)
        {
            return;
        }
        //Converting transform to positions
        foreach (Transform child in m_TransformRoot)
        {
            m_Positions.Add(child.position);
        }

        m_splineVisualizer = new LTSpline(m_Positions.ToArray());

    }

    IEnumerator ExecuteSpline()
    {
        isExecuting = true;
       
        if (m_LeanType == LeanTweenType.animationCurve)
        {
            LeanTween.moveSpline(this.gameObject, m_Positions.ToArray(), m_Duration).setEase(m_AnimCurve);
        }
        else
        {
            LeanTween.moveSpline(this.gameObject, m_Positions.ToArray(), m_Duration).setEase(m_LeanType);
        }

        yield return new WaitForSeconds(m_Duration);
        isExecuting = false;
    }

	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //if (m_splineVisualizer != null)
        //{
        //    m_splineVisualizer.gizmoDraw();
        //}
    }

    void ITweenEvent.Play()
    {
        StartCoroutine(ExecuteSpline());
    }
}
