using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardType : MonoBehaviour {

	public Card card;

	DescriptionControl DC;

	SpriteRenderer spr;

	Animator anmate;

	private bool collected;
	GameObject theGUI;
	BoxCollider2D BC;

	// Use this for initialization
	void Start () {

		theGUI = GameObject.Find ("ImagesDieHere");

		BC = GetComponent<BoxCollider2D> ();

		DC = GameObject.Find ("GameController").GetComponent<DescriptionControl> ();

		this.gameObject.name = card.name;

		Debug.Log (card.name + ": " + card.description);

		spr = GetComponent<SpriteRenderer> ();

		spr.sprite = card.artwork;

		anmate = GetComponent<Animator> ();

		anmate.runtimeAnimatorController = card.animation;
			

	}

	void FixedUpdate(){

		if (collected) {

			this.transform.position = Vector3.MoveTowards (this.transform.position, theGUI.transform.position, 15 * Time.deltaTime);

			if (this.transform.position == theGUI.transform.position)
				Destroy (this.gameObject);

		}


	}

	void OnTriggerEnter2D (Collider2D other){

		if (other.gameObject.tag == "Player") {
			GameControl.Upgrading (card.upgrade, card.DeckNumber);
			DC.UpdateText (card.description);
			DC.UpdateCollection ();
			BC.enabled = false;
			collected = true;
		}

	}

}
