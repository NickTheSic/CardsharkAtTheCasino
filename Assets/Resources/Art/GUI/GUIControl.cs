using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIControl : MonoBehaviour {

	public Sprite[] guis;

	Image image;

	void Start(){

		image = GetComponent<Image> ();
		image.sprite = guis [0];

	}
	
	// Update is called once per frame
	void Update () {

		image.sprite = guis [GameControl.guis];
			
	}
}
