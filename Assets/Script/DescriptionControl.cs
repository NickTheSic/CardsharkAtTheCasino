using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionControl : MonoBehaviour {

	public Text mainDescription;

	public Text CCD;

	AudioSource source;
	public AudioClip pickUp;

	void Start(){

		source = GetComponent<AudioSource> ();

	}

	public void PlaySound(){

		source.PlayOneShot (pickUp, 0.7f);

	}

	public void UpdateText(string t){

		mainDescription.text = t;

	}


	public void UpdateCollection(){

		CCD.text = GameControl.cardsCollected.ToString () + "/" + GameControl.TotalCards.ToString ();

	}

}
