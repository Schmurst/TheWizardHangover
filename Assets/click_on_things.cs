using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class click_on_things : MonoBehaviour {
    public GameObject FadeCanvas = null;
 
    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {
                Debug.Log(hit.transform.gameObject.name);
                FadeCanvas.GetComponent<level_transition>().LoadNextScene(3.0f, 2.0f);
            }
        }

    }
}
