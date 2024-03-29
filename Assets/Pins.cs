using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pins : MonoBehaviour {
	public bool down = false;
	public GameObject myGameController;
	private bool hited;
	private Vector3 pin_startPos;
	private Rigidbody pin_rb;
	public AudioSource HitClip;
	// Use this for initialization
	void Start () {
		pin_rb = GetComponent<Rigidbody>();
		pin_startPos = this.transform.position;
		hited = false;
	}
	void Update(){
		if (this.transform.rotation.w < 0.9 && hited == false) {
			HitClip.Play ();
			hited = true;
		}
	}
	public bool IsDown() {
		if (this.transform.rotation.w < 0.8) {
			return true;
		} 
		else {
			return false;
		}

	}
		
	public void PinReset() {	
		Debug.Log ("-------------mpike----------");
		//Debug.Log ("pin velocity "+pin_rb.velocity);
		//Debug.Log ("this.transform.rotation "+this.transform.rotation);
		this.transform.position = pin_startPos;
		this.transform.rotation = new Quaternion(0, 0, 0, 1);
		pin_rb.velocity = new Vector3(0, 0, 0);
		pin_rb.angularVelocity = new Vector3(0,0,0);
		hited = false;
	}
}