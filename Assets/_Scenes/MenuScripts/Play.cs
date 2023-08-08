
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour {

	public Sprite[] sprites;
	SpriteRenderer spr;

	AudioSource source;
	public AudioClip click;

	void Start(){

		source = GetComponent<AudioSource> ();
		spr = GetComponent<SpriteRenderer> ();

	}

	void OnMouseEnter(){

		spr.sprite = sprites [1];

	}

	void OnMouseExit(){

		spr.sprite = sprites [0];

	}

	void OnMouseDown(){

		spr.sprite = sprites [2];

	}

	void OnMouseUpAsButton(){
		spr.sprite = sprites [0];

		source.PlayOneShot (click, 0.8f);

		if (gameObject.name == "Play")
		SceneManager.LoadScene ("Level1");

		if (gameObject.name == "Quit")
			Application.Quit();

	}
}
