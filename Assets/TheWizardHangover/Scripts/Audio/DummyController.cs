using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            Debug.Log("Pressed 1");
            AudioManager.Instance.PlayAudio(new AudioInfo() { m_Name = "AerisPianoByTannerHelland", m_IsLoop = true, m_Type = SoundType.Ambient} );
            AudioManager.Instance.PlayAudio(new AudioInfo() { m_Name = "AerisPianoByTannerHelland", m_tagName = "Aeris", m_IsLoop = true, m_Type = SoundType.Ambient });
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.Instance.PlayAudio(new AudioInfo() { m_Name = "NokiaTune", m_IsLoop = false, m_Type = SoundType.Effect });

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AudioManager.Instance.PlayAudio(new AudioInfo() { m_Name = "Rooster", m_IsLoop = false, m_Type = SoundType.Voice });

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AudioManager.Instance.Stop(SoundType.Ambient);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AudioManager.Instance.Stop("Aeris");
        }
    }
}
