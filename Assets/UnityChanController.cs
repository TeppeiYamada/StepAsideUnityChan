using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

	Animator myAnimator;

	private Rigidbody myRigid;

	private float forwardForce = 800.0f;

	private float turnForce = 500.0f;

	private float upForce = 500.0f;

	private float movableRange = 3.4f;

	private float coefficient = 0.95f;

	private bool isEnd = false;

	private GameObject stateText;

	private GameObject scoreText;

	private int score = 0;

	private bool isLButtonDown = false;

	private bool isRButtonDown = false;

	// Use this for initialization
	void Start () {

		this.myAnimator = GetComponent<Animator> ();

		this.myAnimator.SetFloat ("Speed", 1);

		this.myRigid = GetComponent<Rigidbody> ();

		this.stateText = GameObject.Find ("GameResultText");

		this.scoreText = GameObject.Find ("ScoreText");
		
	}

	// Update is called once per frame
	void Update () {

		this.myRigid.AddForce (this.transform.forward * this.forwardForce);

		if((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -movableRange < this.transform.position.x){

			this.myRigid.AddForce (-turnForce, 0, 0);

		}else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < movableRange){

			this.myRigid.AddForce (turnForce, 0, 0);

		}

		if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")){

			this.myAnimator.SetBool ("Jump", false);

		}

		if(Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f){

			this.myAnimator.SetBool ("Jump", true);

			this.myRigid.AddForce (this.transform.up * upForce);

		}

		if(this.isEnd){

			this.turnForce *= coefficient;
			this.upForce *= coefficient;
			this.forwardForce *= coefficient;
			this.myAnimator.speed *= coefficient;

		}

	}

	void OnTriggerEnter(Collider other){

		if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag"){

			this.isEnd = true;

			this.stateText.GetComponent<Text>().text = "GameOver";

		}

		if(other.gameObject.tag == "GoalTag"){

			this.isEnd = true;

			this.stateText.GetComponent<Text> ().text = "CLEAR!!";

		}

		if(other.gameObject.tag == "CoinTag"){

			this.score += 10;

			this.scoreText.GetComponent<Text> ().text = "Score " + this.score + "pt";

			GetComponent<ParticleSystem> ().Play();

			Destroy (other.gameObject);

		}

	}

	public void GetMyJumpButtonDown() {
		
		if (this.transform.position.y < 0.5f) {
			
			this.myAnimator.SetBool ("Jump", true);
			this.myRigid.AddForce (this.transform.up * this.upForce);

		}
	}
		
	public void GetMyLeftButtonDown() {
		
		this.isLButtonDown = true;

	}


	public void GetMyLeftButtonUp() {
		
		this.isLButtonDown = false;

	}


	public void GetMyRightButtonDown() {
		
		this.isRButtonDown = true;

	}


	public void GetMyRightButtonUp() {
		
		this.isRButtonDown = false;

	}

}
