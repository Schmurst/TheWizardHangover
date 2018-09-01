using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNextSceneButton : MonoBehaviour, SimpleTween.ITweenEvent
{
    void SimpleTween.ITweenEvent.Play()
    {
        var levelTrans = FindObjectOfType<level_transition>();
        levelTrans.EnableButton();
    }
}