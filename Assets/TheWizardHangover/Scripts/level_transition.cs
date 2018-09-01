using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class level_transition : MonoBehaviour {
    public Image[] Images;
    public int NextSceneIndex = 1;
    public bool Fading = false;
    public GameObject theImages = null;
    public GameObject Flyer = null;

    public void EnableButton()
    {
        GetComponentInChildren<Button>(true).gameObject.SetActive(true);
    }

    public void ActuallyLoadNextScene()
    {
        Debug.Log("ActuallyLoadNextScene");
        LoadNextScene(3.0f, 2.0f);
    }

    public void LoadNextScene(float aFadeOutTime, float aFadeInTime)
    {
        if (Fading) {
            Debug.Log("Fading early out");
            return;
        }
        StartFade(aFadeOutTime, aFadeInTime);
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
        NextSceneIndex = 1;
        ResetFade();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ResetFade()
    {
        Debug.Log("ResetFade");
        Fading = false;
        theImages.SetActive(false);
        Images = transform.Find("Images").gameObject.GetComponentsInChildren<Image>(true);
        foreach (Image image in Images) {
            image.enabled = false;
        }
        Flyer = GameObject.Find("Flyer") ? GameObject.Find("Flyer").gameObject : null;
        if (Flyer)
            Flyer.SetActive(false);
    }

    private void RenderImage(float aAlpha, int active_image_index)
    {
        //Debug.Log("RenderImage ("+ active_image_index+")" + aAlpha);
        // Canvas
        GetComponentInChildren<CanvasGroup>().alpha = aAlpha;
        foreach (Image image in Images) {
            image.enabled = false;
        }
        Images[active_image_index].enabled = true;
    }

    private IEnumerator Fade(float aFadeOutTime, float aFadeInTime)
    {
        //Debug.Log("Fade");
        float t = 0.0f;
        while (t < 1.0f) {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t + Time.deltaTime / aFadeOutTime);
            int active_image_index = Mathf.Clamp((int)(Images.Length * t),0, Images.Length-1);
            RenderImage(t, active_image_index);
        }
        if(NextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(NextSceneIndex++);
        } 
        while (t > 0.0f) {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t - Time.deltaTime / aFadeInTime);
            int active_image_index = Mathf.Clamp((int)(Images.Length * t), 0, Images.Length - 1);
            RenderImage(t, active_image_index);
        }
        
        Debug.Log("Finished fade");
        ResetFade(); 
    }

    private void StartFade(float aFadeOutTime, float aFadeInTime)
    {
        Debug.Log("StartFade");
        // last level we present flyer, then restart
        if (Flyer) {
            if(Flyer.active) {
                NextSceneIndex = 0;
            } else {
                Flyer.SetActive(true);
                return;
            }
        }

        Fading = true;
        theImages.SetActive(true);
        GetComponentInChildren<Button>().gameObject.SetActive(false);
        StartCoroutine(Fade(aFadeOutTime, aFadeInTime));
    }
}
