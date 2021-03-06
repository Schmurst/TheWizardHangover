﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleTween
{
	public enum EventType
	{
		none = 0,
		onEnable = 1,
		pointerUp = 2,
		pointerDown = 4,
		pointerEnter = 8,
		pointerExit = 16,
		pointerClick = 32,
	}
	
	public class EventTweener : Tweener, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
								IPointerUpHandler, IPointerClickHandler
	{
		[SerializeField] EventType[] 	m_events;
        [SerializeField] float m_cooldownSeconds = 0f;
        [SerializeField] bool m_isOneShot = false;

        protected bool m_hasBeenPlayed;
        protected float m_timeOfLastEvent;

		//--------------------------------------------------------------------------------
		public new string name {get {return "";}}
		//--------------------------------------------------------------------------------
		void OnEnable() { Execute (EventType.onEnable);}
		public void OnPointerEnter	(PointerEventData eventData){Execute (EventType.pointerEnter);}
		public void OnPointerExit 	(PointerEventData eventData){Execute (EventType.pointerExit);}
		public void OnPointerDown 	(PointerEventData eventData){Execute (EventType.pointerDown);}
		public void OnPointerUp  	(PointerEventData eventData){Execute (EventType.pointerUp);}
		public void OnPointerClick 	(PointerEventData eventData){Execute (EventType.pointerClick);}
		//--------------------------------------------------------------------------------
        protected virtual bool ShouldPlayEvent(EventType _type)
		{
			if (_type == EventType.none)
				return false;

            if (m_isOneShot && m_hasBeenPlayed)
                return false;

            if (Time.time < m_timeOfLastEvent + m_cooldownSeconds)
                return false;

			for (int i = 0; i < m_events.Length; i++)
				if (m_events[i] == _type)
					return true;

			return false;
		}
		
		//--------------------------------------------------------------------------------
        protected virtual void Execute(EventType _type)
		{
			if(ShouldPlayEvent(_type))
            {
                Play();
                m_hasBeenPlayed = true;
                m_timeOfLastEvent = Time.time;            
            }
		}
	}
}