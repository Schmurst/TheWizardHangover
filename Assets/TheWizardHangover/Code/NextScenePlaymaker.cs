using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScenePlaymaker : MonoBehaviour {

    public void EnableNextScene()
    {
        var levelTrans = FindObjectOfType<level_transition>();
        levelTrans.EnableButton();
    }
}
