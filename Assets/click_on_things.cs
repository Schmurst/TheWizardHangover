using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class click_on_things : MonoBehaviour {
    public GameObject FadeCanvas = null;
    public Image[] Images;
    private static click_on_things m_Instance = null;
    private string m_LevelName = "";
    private int m_LevelIndex = 0;
    public int ActiveImageIndex = 0;
    private bool m_Fading = false;

    private static click_on_things Instance
    {
        get
        {
            if (m_Instance == null) {
                m_Instance = (new GameObject("click_on_things")).AddComponent<click_on_things>();
            }
            return m_Instance;
        }
    }

    public static bool Fading
    {
        get { return Instance.m_Fading; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(FadeCanvas);
        m_Instance = this;
        ActiveImageIndex = 0;
        FadeCanvas.SetActive(false);
        Images = FadeCanvas.GetComponentsInChildren<Image>();
    }

    private void DrawQuad(Color aColor, float aAlpha)
    {
        // Canvas
        FadeCanvas.SetActive(true);
        FadeCanvas.GetComponent<CanvasGroup>().alpha = aAlpha;
        foreach (Image image in Images) {
            image.enabled = false;
        }
        Images[ActiveImageIndex].enabled = true;
    }

    private IEnumerator Fade(float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        float t = 0.0f;
        while (t < 1.0f) {
            ActiveImageIndex = (int)(Images.Length * t);

            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t + Time.deltaTime / aFadeOutTime);
            DrawQuad(aColor, t);
        }
        if (m_LevelName != "")
            SceneManager.LoadScene(m_LevelName);
        else
            SceneManager.LoadScene(m_LevelIndex);
        while (t > 0.0f) {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t - Time.deltaTime / aFadeInTime);
            DrawQuad(aColor, t);
        }
        m_Fading = false;
    }
    [EditorDebugMethod]
    private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        m_Fading = true;
        StartCoroutine(Fade(aFadeOutTime, aFadeInTime, aColor));
    }

    public static void LoadScene(string aLevelName, float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        if (Fading) return;
        Instance.m_LevelName = aLevelName;
        Instance.StartFade(aFadeOutTime, aFadeInTime, aColor);
    }
    public static void LoadScene(int aLevelIndex, float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        if (Fading) return;
        Instance.m_LevelName = "";
        Instance.m_LevelIndex = aLevelIndex;
        Instance.StartFade(aFadeOutTime, aFadeInTime, aColor);
    }
   

    // Use this for initialization
    void Start () {
        FadeCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {
                Debug.Log(hit.transform.gameObject.name);
                LoadScene("bedroom",3.0f,2.0f,Color.red);
            }
        }
    }
}
