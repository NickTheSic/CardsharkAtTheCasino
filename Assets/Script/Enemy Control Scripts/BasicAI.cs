using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//          "Basic"

public class BasicAI : MonoBehaviour {

	public int speed = 2;

	public bool facingRight;

	public bool checkSpotted;

	Animator anim;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (GameControl.isSpotted) {

			anim.Play ("NewSpotted");

			checkSpotted = true;

			speed = 0;

		}

		if (!GameControl.isSpotted && checkSpotted) {
			speed = 2;
			checkSpotted = false;

			anim.Play ("NewGuardWalking");

		}

		if (!facingRight) {
			transform.Translate (Vector2.left * speed * Time.deltaTime);
			transform.localScale = new Vector2 (2, 2);
		}
		if (facingRight) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.localScale = new Vector2 (-2, 2);
		}
		
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "AIStop")
			StartCoroutine ("StopNSpin");

	}


	private IEnumerator StopNSpin(){

		speed = 0;

		anim.Play ("NewSurvey");

		yield return new WaitForSeconds (2.7f);

		anim.speed = 1;

		facingRight = !facingRight;

		speed = 2;

		yield return null;

	}
}
