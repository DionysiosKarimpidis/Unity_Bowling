using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraScript : MonoBehaviour {

	public GameObject Sphere_ball;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - Sphere_ball.transform.position;
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = Sphere_ball.transform.position + offset;
		
	}
}
