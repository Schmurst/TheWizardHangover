using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_scrolly : MonoBehaviour {
    public float mouse_percentage;

    // Use this for initialization
    void Start () {
		
	}

    [Range(0.0f, 0.3f)]
    public float mouse_buffer_on_edge_of_screen = 0.1f;
    public float max_scroll_speed = 0.05f;
    public Vector2 scene_extremities = new Vector2(-10.0f, 10.0f);

    // Update is called once per frame
    void Update () {
        Camera c = Camera.main;
        Vector3 offset = new Vector3(0.0f,0.0f,0.0f);

        mouse_percentage = Input.mousePosition.x / c.pixelWidth;
        if(mouse_percentage < mouse_buffer_on_edge_of_screen) {
            // move leftwards, faster if nearer edge of screen
            float speed = (mouse_buffer_on_edge_of_screen - mouse_percentage) / mouse_buffer_on_edge_of_screen;
            offset = new Vector3(-speed * max_scroll_speed, 0.0f, 0.0f);
        } else if(mouse_percentage > 1.0f - mouse_buffer_on_edge_of_screen) {
            // move rightwards, faster if nearer edge of screen
            float speed = (mouse_percentage - 1.0f + mouse_buffer_on_edge_of_screen) / mouse_buffer_on_edge_of_screen;
            offset = new Vector3(speed * max_scroll_speed, 0.0f, 0.0f);
        }

        transform.position += offset;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, scene_extremities.x, scene_extremities.y), transform.position.y, transform.position.z);
    }
}
