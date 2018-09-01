using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class level_transition : MonoBehaviour {
    public Image[] Images;
    public int NextSceneIndex = 1;
    public bool Fading = false;

    public void LoadNextScene(float aFadeOutTime, float aFadeInTime)
    {
        if (Fading) return;
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
        Fading = false;
        gameObject.SetActive(false);
        Images = GetComponentsInChildren<Image>();
    }

    private void DrawQuad(float aAlpha, int active_image_index)
    {
        // Canvas
        GetComponent<CanvasGroup>().alpha = aAlpha;
        foreach (Image image in Images) {
            image.enabled = false;
        }
        Images[active_image_index].enabled = true;
    }

    private IEnumerator Fade(float aFadeOutTime, float aFadeInTime)
    {
        float t = 0.0f;
        while (t < 1.0f) {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t + Time.deltaTime / aFadeOutTime);
            int active_image_index = Mathf.Clamp((int)(Images.Length * t),0, Images.Length-1);
            DrawQuad(t, active_image_index);
        }
        SceneManager.LoadScene(NextSceneIndex++);
        while (t > 0.0f) {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t - Time.deltaTime / aFadeInTime);
            int active_image_index = Mathf.Clamp((int)(Images.Length * t), 0, Images.Length - 1);
            DrawQuad(t, active_image_index);
        }
        ResetFade();
    }

    private void StartFade(float aFadeOutTime, float aFadeInTime)
    {
        Fading = true;
        gameObject.SetActive(true);
        StartCoroutine(Fade(aFadeOutTime, aFadeInTime));
    }
}
