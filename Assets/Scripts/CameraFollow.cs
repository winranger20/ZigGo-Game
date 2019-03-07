using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject ball;
    // Distance between the camera and ball
    Vector3 offset;
    // Camera will change its position to follow the ball
    public float lerpRate; 
    public bool gameOver;

	// Use this for initialization
	void Start ()
    {
        // Distance from the ball
        offset = ball.transform.position - transform.position; 
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!gameOver)
        {
            Follow();
        }
	}

    // Camera follow
    void Follow() 
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = ball.transform.position - offset;
        pos = Vector3.Lerp(pos, targetPos, lerpRate * Time.deltaTime);
        transform.position = pos;
    }
}
