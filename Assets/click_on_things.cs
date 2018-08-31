using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class click_on_things : MonoBehaviour {

    private static click_on_things m_Instance = null;
    private string m_LevelName = "";
    private int m_LevelIndex = 0;
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
        m_Instance = this;
    }

    private void DrawQuad(Color aColor, float aAlpha)
    {
        if (!m_Instance.gameObject.GetComponent<GUITexture>()) {
            m_Instance.gameObject.AddComponent<GUITexture>();
        }
        m_Instance.gameObject.transform.localScale = Vector3.zero;
        Texture2D tex2d = new Texture2D(1, 1);
        tex2d.SetPixels(new Color[1] { Color.white });
        tex2d.Apply();
        m_Instance.gameObject.GetComponent<GUITexture>().texture = tex2d;

        m_Instance.GetComponent<GUITexture>().pixelInset = new Rect(0, 0, Screen.width, Screen.height);
        m_Instance.GetComponent<GUITexture>().color = new Color(aColor.r, aColor.g, aColor.b, aAlpha);
    }

    private IEnumerator Fade(float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        float t = 0.0f;
        while (t < 1.0f) {
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
