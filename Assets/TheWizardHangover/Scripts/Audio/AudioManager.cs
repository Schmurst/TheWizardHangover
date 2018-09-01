using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType { Ambient = 0, Effect = 1, Voice = 2 };

[System.Serializable]
public class AudioInfo
{
    public SoundType m_Type;
    public string m_Name;
    public string m_tagName;
    public bool m_IsLoop;
}

public class AudioManager : Singleton<AudioManager> {

    private AudioListener m_audioListener;

    [SerializeField]
    private AudioMixer m_AudioMixer;
    [SerializeField]
    private AudioMixerGroup[] m_audioMixerGroups;
    [SerializeField]
    private List<GameObject> m_playingList = new List<GameObject>();

    #region API
    /// <summary>
    /// Play single audio from Resources in a single channel
    /// 
    /// </summary>
    /// <param name="audioName">Audio name without etension</param>
    /// <param name="forcePlay">Stop current audio to be played and play new one or finish the current sound to be played and then play the following one</param>
    public void PlayAudio(AudioInfo ai)
    {
        
        try
        {            
            AudioClip audioclip = Resources.Load(String.Format("Audio/{0}", ai.m_Name)) as AudioClip;
            if (audioclip == null)
            {
                Debug.LogError("AudioManager: " + ai.m_Name + " not found");
                //Stop execution
                return;
            }

            string currName = ai.m_tagName == String.Empty ? "GameSound" : ai.m_tagName;
            GameObject go = new GameObject(currName);
            //Tagging the go to remove specific sounds from specific channels
            go.tag = ai.m_Type.ToString();
            //Attaching to the sound manager
            go.transform.SetParent(gameObject.transform, false);
            //Adding audiosource to new go
            AudioSource source = go.AddComponent<AudioSource>();

            source.playOnAwake = true;
            source.clip = audioclip;
            source.loop = ai.m_IsLoop;

            //Forwardng the sound to the appropriate channel
            switch (ai.m_Type)
            {
                case SoundType.Ambient:
                    source.outputAudioMixerGroup = m_audioMixerGroups[1];
                    break;
                case SoundType.Effect:
                    source.outputAudioMixerGroup = m_audioMixerGroups[2];
                    break;
                case SoundType.Voice:
                    source.outputAudioMixerGroup = m_audioMixerGroups[3];
                    break;
                default:
                    break;
            }

            source.Play();
            m_playingList.Add(go);

        }
        catch (System.Exception ex)
        {
            Debug.LogError("AudioManager: " + ex.Message);                
        }
        
    }

    /// <summary>
    /// Stop sounds playing on a specific channel
    /// </summary>
    /// <param name="soundType">Channel with the sounds to stop</param>
    public void Stop(SoundType soundType)
    {
        GameObject[] soundsInChannel = GameObject.FindGameObjectsWithTag(soundType.ToString());

        for (int i = 0; i < soundsInChannel.Length; i++)
        {
            AudioSource audioSource = soundsInChannel[i].GetComponent<AudioSource>();
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    /// <summary>
    /// This function stop a SINGLE audio if being played
    /// </summary>
    /// <param name="audioName"></param>
    public void Stop(string audioName)
    {
        try
        {
            GameObject go = m_playingList.Where(x => x.name == audioName).FirstOrDefault();
            if (go != null)
            {
                AudioSource audiosource = go.GetComponent<AudioSource>();
                if (audiosource != null)
                {
                    audiosource.Stop();
                    //Will be removed in the update function
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning("AudioManager: " + audioName + " not found when calling Stop()");
        }
        
    }
    #endregion

    #region UnityFunctions
    // Use this for initialization
    protected override void Awake () {

        //m_audioListener = gameObject.GetComponent<AudioListener>();
        //if (m_audioListener != null)
        //{
        //    //Adding audioListener
        //    m_audioListener = gameObject.AddComponent<AudioListener>();
        //}
        
        if (m_AudioMixer == null)
        {
            Debug.LogError("AudioManager: Please attach audioMixer to the script");
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Automatically destroy not playing audiosources
        for (int i = 0; i < m_playingList.Count; i++)
        {
            AudioSource m_As = m_playingList[i].gameObject.GetComponent<AudioSource>();
            if (m_As != null && !m_As.isPlaying)
            {
                Destroy(m_playingList[i]);
                m_playingList.RemoveAt(i);
                i--;
            }
        }
    }
    #endregion
}
