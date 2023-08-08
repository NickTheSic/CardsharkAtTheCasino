using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

	//Speed variables
	public float speed;
	public float runSpeed;
	public float s;
	public float r;

	//Is the upgrade unlocked and is he in range to do it
	public bool canClimb;
	public bool canHide;
	public bool isHiding;
	public bool canClimbDown;

	public bool didPressUp;
	public bool isRunning;
	public bool isClimbing;


	public bool checkSpotted;

	public Vector3 CurrentCheckpoint;

	//Controls the text box for now
	public DescriptionControl DC;

	//My way of setting up character movement
	public float ZLoc;
	public float XLoc;
	public float YLoc;
	public Vector3 playerLoc;

	private Rigidbody2D rb;

	//Animator
	Animator anim;
	public int keyDown;

	AudioSource source;
	public AudioClip card;
	public AudioClip jump;
	public AudioClip guard;
	public AudioClip slots;

	// Use this for initialization
	void Start () {

		source = GetComponent<AudioSource> ();

		CurrentCheckpoint = this.transform.position;

		//Get the animator
		anim = GetComponent<Animator>();
		keyDown = 0;
		anim.Play ("Idle");

		//Get and set the current location of the character
		playerLoc = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		XLoc = playerLoc.x;
		YLoc = playerLoc.y;
		ZLoc = playerLoc.z;

		rb = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (GameControl.cardsCollected == GameControl.TotalCards) {

			DC.UpdateText ("You collected all the cards!");

			StartCoroutine ("EndGame");

		}

		//Fixes the flying away
		if (!canClimb && !canClimbDown)
			rb.gravityScale = 2;
		
			//Old movement
			HandleMovement ();
			//New movement, will allow jumping
			//RBMovement();

			HandleJumps ();

			HandleAnimation ();
			HandleHiding ();


		if (!checkSpotted && GameControl.isSpotted) {

			checkSpotted = true;

			source.PlayOneShot (guard, 0.7f);

			speed = 0;

			StartCoroutine ("Checkpoint");

		}

	}

	private IEnumerator EndGame(){

		yield return new WaitForSeconds (3);

		GameControl.Restart ();

		SceneManager.LoadScene ("Menu");

	}

	void FixedUpdate(){

		isGrounded = IsGrounded();

	}

	//Ground points transform

	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask whatIsGround;

	private bool isGrounded;

	private bool IsGrounded(){

		if (rb.velocity.y <= 0) {

			foreach (Transform point in groundPoints) 
			{

				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);

				for (int i = 0; i < colliders.Length; i++) {

					if (colliders[i].gameObject != gameObject) {
						return true;
					}
				}
			}
		}
		return false;
	}

	//Handle the jumping
	private void HandleJumps(){

		if (!canClimb && !isHiding && isGrounded && GameControl.jump) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				rb.AddForce (new Vector2 (0, 400));
				source.PlayOneShot (jump, 0.7f);
			}
		}


	}

	/*private void RBMovement(){

		float ns;

		if (!isHiding)
			ns = Input.GetAxis ("Horizontal");

		if (ns > 0)
			transform.localScale = new Vector2 (2, 2);

		if (ns < 0)
			transform.localScale = new Vector2 (-2, 2);

		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift))
			isRunning = true;

		if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift))
			isRunning = false;

		if (!isRunning)
			ns *= speed;
		if (isRunning)
			ns *= runSpeed;

		rb.velocity = new Vector2 (ns, rb.velocity.y);

	}*/


	private void HandleHiding (){

		if (canHide) {

			if (Input.GetKeyDown (KeyCode.E)) {

				isHiding = true;
				speed = 0;
				transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 0.2f, 2);
				rb.gravityScale = 0;

			}


			if (Input.GetKeyUp (KeyCode.E)) {

				anim.Play ("Idle");

				isHiding = false;
				speed = s;
				transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - 0.2f, -2);
				rb.gravityScale = 2;

			}

		}

	}


	private void HandleMovement(){

		//Make sure the character is at the location of the variables
		//playerLoc = new Vector3 (XLoc, YLoc, ZLoc);
		//this.transform.position = playerLoc;

		//Move left
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {

			//Might switch back to translate
			//XLoc -= speed * Time.deltaTime;
			this.transform.Translate (Vector3.left * speed * Time.deltaTime);
			this.transform.localScale = new Vector3 (-2, 2, 1);

		}

		//Move right
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {

			//might switch back to translate
			//XLoc += speed * Time.deltaTime;
			this.transform.Translate (Vector3.right * speed * Time.deltaTime);
			this.transform.localScale = new Vector3 (2, 2, 1);

		}

		//Run upgrade unlocked?
		if (GameControl.run) {

			if (!isHiding) {
				if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
					speed = r;
					isRunning = true;
				}

				if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift)) {
					isRunning = false;
					speed = s;
				}
			}
		}

		if (canClimb) {

			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {

				//Play animation
				this.transform.Translate (Vector3.up * speed * Time.deltaTime);
				//YLoc += speed * Time.deltaTime;

				isClimbing = true;


			}

			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {

				//Play animation
				this.transform.Translate (Vector3.down * speed * Time.deltaTime);
				//YLoc -= speed * Time.deltaTime;

			}
		}
		if (canClimbDown) {

			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {

				//Play animation
				this.transform.Translate (Vector3.down * speed * Time.deltaTime);
				//YLoc -= speed * Time.deltaTime;
			}
		}
			
	}


	//
	//Animation Handler
	//
	private void HandleAnimation(){

		if (isHiding) {
			anim.Play("Idle");
		}

		//Walking

		if (!isHiding) {
			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) {

				if (!isRunning)
					anim.Play ("Walking");

				if (isRunning)
					anim.Play ("Running");

				anim.speed = 1;


			}

			//Idle from walking
			if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.RightArrow)) {
				
				anim.Play ("Idle");
			}
		}

		/*if (isClimbing) {

			anim.speed = 0;
			anim.Play ("Idle");

		}*/

		//Climbing

			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {

			if (canClimb) {
				anim.Play ("Climbing");
				anim.speed = 1;

				didPressUp = true;

			}

		}

		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.UpArrow)) {

			if (didPressUp) {
				anim.speed = 0;
				didPressUp = false;
			}

		}

		if (canClimb || canClimbDown) {

			if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {

				anim.Play ("Climbing");
				anim.speed = 1;

			}

			if (Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.DownArrow)) {

				anim.speed = 0;

			}

		}


	}


	private void UpdateCheckpoint(){

		CurrentCheckpoint = this.transform.position;
		CurrentCheckpoint.y += 0.2f;

	}

	//TRIGGER CONTROLS
	void OnTriggerEnter2D (Collider2D other){

		if (other.gameObject.tag == "Slots") {

			source.PlayOneShot (slots, 0.5f);

		}

		//Spotted area
		if (other.gameObject.tag == "Spotted" && !isHiding) {

			DC.UpdateText("Spotted!");
			GameControl.isSpotted = true;

		}

		if (other.gameObject.tag == "Card") {

			UpdateCheckpoint ();

			source.PlayOneShot (card, 0.7f);

			//Play pickup sound;

		}

		//Enter climable area
		if (other.gameObject.tag == "Climb") {

			if (GameControl.climb) {
				canClimb = true;
				DC.UpdateText ("Press W/Up to climb");
				rb.gravityScale = 0;
			}		if (!GameControl.climb) {
				DC.UpdateText ("You can't climb yet");
			}

		}
		//Climb down control
		if (other.gameObject.tag == "ClimbDown") {

			if (GameControl.climb) {
				canClimbDown = true;
				DC.UpdateText ("Press S/Down to climb down");
				rb.gravityScale = 0;
			}

			if (!GameControl.climb) {
				DC.UpdateText ("You can't climb yet");
			}

		}

		//Enter object you can hide behind
		if (other.gameObject.tag == "Hide") {

			if (GameControl.hide) {
				canHide = true;
				DC.UpdateText ("Press E to hide");
			}

			if (!GameControl.hide) {
				DC.UpdateText ("You can't hide yet");
			}

		}
			


	}

	void OnTriggerStay2D (Collider2D other){

		if (other.gameObject.tag == "Spotted" && !isHiding) {

			DC.UpdateText("Spotted!");
			GameControl.isSpotted = true;

		}

		if (other.gameObject.tag == "Climb" || other.gameObject.tag == "ClimbDown") {
			rb.gravityScale = 0;
		}

	}


	void OnTriggerExit2D (Collider2D other){

		DC.UpdateText ("");

		if (other.gameObject.tag == "Climb") {
			canClimb = false;
			rb.gravityScale = 2;
			isClimbing = false;
		}
		if (other.gameObject.tag == "ClimbDown") {
			canClimbDown = false;
			rb.gravityScale = 2;
		}

		if (other.gameObject.tag == "Hide") {
			
			canHide = false;

		}

	}


	private IEnumerator Checkpoint(){

		yield return new WaitForSeconds (2f);

		GameControl.ResetSpotted ();

		checkSpotted = false;

		isHiding = false;
		isClimbing = false;

		speed = s;

		//Get current checkpoint
		this.transform.position = CurrentCheckpoint;

	}
	 

}
