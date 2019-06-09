using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBall : MonoBehaviour {
	public Vector3 ball_startPos;
	public Rigidbody ball_rb;
	public float thrust;
	public GameObject[] pin;
	public Pins[] pins;
	public int hit;

	private bool throwFlag = false;
	private int score1 = 0; //1h mpalia
	private int score2 = 0; //2h mpalia
	private int tempSum;//voithitiki metavliti gia na vrw to teliko sum kai na to apothikeuso se pinaka(ton sum)
	private int k = 0; //metritis gia ton pinaka sum
	private int[] sum = new int[20]; // sum kai gia tous duo paiktes(10 frames x 2 paiktes)
	private int[] tempScore = new int[40]; // mpalies kai gia tous duo paiktes: 10 frames x 2 mpalies se kathe frame x 2 paiktes)
	private int c = 0; //metritis gia ton pinaka tempScore
	private int j = 0; //metritis gia to ScoreText
	//private int val = 0; //temp value metavliti gia swsto upologismo twn epimerous sum
	public Text finalScore1; //teliko score player 1
	public Text finalScore2; //teliko score player 1
	private bool wasStrike = false;


	public Text[] ScoreText;
	public Text[] sumText;
	public AudioSource HitFloorClip;

	// Use this for initialization
	void Start () 
	{
		ball_rb = GetComponent <Rigidbody>();
		ball_startPos = this.transform.position;
		throwFlag = false;
		hit = 0;
	}

	void Update() {
		if (!throwFlag) {

			if (Input.GetKey ("left")) {
				Vector3 position = this.transform.position;
				position.x = position.x - 0.03f;
				this.transform.position = position;
			}
			if (Input.GetKey ("right")) {
				Vector3 position = this.transform.position;
				position.x = position.x + 0.03f;
				this.transform.position = position;
			}
		}
	}

	void OnMouseDown () {

		if (!throwFlag) {
			//Debug.Log ("on mouse Down score1: "+score1);
			//Debug.Log ("on mouse Down score2: "+score2);
			ball_rb.AddForce (0, 0, thrust * 100, ForceMode.Impulse);
			ball_rb.useGravity = true;
			throwFlag = true; 
			StartCoroutine(ResetBallTime());
		}
	}
		

	IEnumerator ResetBallTime()
	{
		yield return new WaitForSeconds(5);
		ResetBall ();
	}

	public void ResetBall()
	{
		ball_rb.useGravity = false;
		this.transform.position = ball_startPos;
		this.transform.rotation = new Quaternion(0, 0, 0, 1);
		ball_rb.velocity = new Vector3(0, 0, 0);
		ball_rb.angularVelocity = new Vector3(0,0,0);
		throwFlag = false;
		//Debug.Log ("ResetBall score1: "+score1);
		//Debug.Log ("ResetBall score2: "+score2);
		ResetPins();
		hit = hit + 1;
	}

	public void ResetPins() {		//and count score
		
		Debug.Log ("hit= "+hit);
		if (hit % 2 == 0) {
			score1 = 0;
			for (int i = 0; i < 10; i++) {
				if (pins [i].IsDown ()) {
					score1++;
					ScoreText [hit].text = score1.ToString ();
					//Debug.Log ("score 1 : " +score1);
					pin [i].SetActive (false);
				}
			}
			if (score1 == 10) {
				wasStrike = true;
				score2 = 0;
				hit++;
				k++;
				for (int i = 0; i < 10; i++) {
					pins [i].PinReset ();
					pin [i].SetActive (true);
				}
			}
		}
		else {
			tempSum = 0;
			score2 = 0;
			for (int i = 0; i < 10; i++) {
				if (pins[i].IsDown ()) {
					tempSum++;
					pin [i].SetActive (true);
				}
			}
			if (wasStrike) {
				Debug.Log ("wasStrike K = " + k);
				Debug.Log ("wasStrike sum[k] = " + sum [k]);
				sum [k - 2] = 10 + tempSum;
				sum [k] = sum [k - 2] + tempSum;
				wasStrike = false;
			} else {
				sum [k] = tempSum;
			
				//Debug.Log ("tempSum = "+tempSum);
				if (k % 2 == 0) {								
					if (k == 0) {
						sumText [k].text = sum [k].ToString ();
					} else {
						sum [k] = sum [k] + sum [k - 2];
						sumText [k].text = sum [k].ToString ();
					}
				} else {
					if (k == 1) {
						sumText [k].text = sum [k].ToString ();
					} else {
						sum [k] = sum [k] + sum [k - 2];
						sumText [k].text = sum [k].ToString ();
					}
				}
			}
			score2 = tempSum - score1;
			ScoreText [hit].text = score2.ToString ();
			k++;
			Debug.Log ("auksisi k = "+k);
			//Debug.Log ("sum[ k = "+k);

			Debug.Log ("sum is : " +sum[k-1]);
			for (int i = 0; i < 10; i++) {
				pins [i].PinReset();
			}
		}
		//Debug.Log ("score 1  is : " +score1);
		//Debug.Log ("score 2  is : " +score2);
		//Debug.Log ("sum [k]  is : " +sum[k-1]);
		tempScore [c] = score1;
		tempScore [c+1] = score2;
		//Debug.Log ("tempScore  is : " + tempScore[c] +" - next tempscore= "+tempScore[c+1]);
		//sumText [k].text = tempSum.ToString ();
		/*if (j % 2 == 0) {
			ScoreText [j].text = score1.ToString ();
		} else {
			ScoreText [j].text = score2.ToString ();
		}*/
		c++;
		j++;

		}
	public void ExitGame(){
		Application.Quit();
	}
	}
	