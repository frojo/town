using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    // these are two ratios for x and y deadzone size
    public Vector2 deadZone;

    public Transform target;
    public Camera cam;

    // these are both half-sizes of the camera (from center of screen to edge)
    float height;
    float width;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        height = cam.orthographicSize;
        width = height * cam.aspect;
	}

    // Update is called once per frame
    void LateUpdate() {
        if (target == null)
        {
            return;
        }
        
        float deadZoneSizeX = width * deadZone.x;
        float newX = transform.position.x;
        if (target.position.x > transform.position.x + deadZoneSizeX)
        {
            newX = target.position.x - deadZoneSizeX;
        } else if (target.position.x < transform.position.x - deadZoneSizeX) {
            newX = target.position.x + deadZoneSizeX;
        }

        float deadZoneSizeY = height * deadZone.y;
        float newY = transform.position.y;
        if (target.position.y > transform.position.y + deadZoneSizeY)
        {
            newY = target.position.y - deadZoneSizeY;
        }
        else if (target.position.y < transform.position.y - deadZoneSizeY)
        {
            newY = target.position.y + deadZoneSizeY;
        }

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
