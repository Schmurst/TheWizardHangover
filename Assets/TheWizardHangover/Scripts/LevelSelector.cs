using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioManager.Instance.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CupboardLevel()
    {
        SceneManager.LoadScene("Cupboard_WB");
    }

    public void BathroomLevel()
    {
        SceneManager.LoadScene("Bathroom_WB");
    }

    public void BedroomLevel()
    {
        SceneManager.LoadScene("Bedroom_WB");
    }

    public void KitchenLevel()
    {
        SceneManager.LoadScene("Kitchen_WB");
    }

    public void LivingRoomLevel()
    {
        SceneManager.LoadScene("Living_Room_WB");
    }

}
