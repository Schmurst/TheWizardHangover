using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    static T m_me;

    public static T Me { get { return m_me; }}

    void Awake()
    {
        m_me = this as T;
    }
}