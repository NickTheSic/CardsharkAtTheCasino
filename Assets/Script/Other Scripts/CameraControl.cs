using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public GameObject _player;
	private float xOff;
	private float yOff;
	private float zOff;

	//starting values of x = 0, y = 0, z = -10
	// final x is end of the level and the Y will be the top of the screen

	// Use this for initialization
	void Start () {

		xOff = 0; //this.transform.position.x;
		yOff = 0; //this.transform.position.y;
		zOff = this.transform.position.z;

	}
	
	// Update is called once per frame
	void Update () {

		if (_player.transform.position.x < this.transform.position.x)
			this.transform.position = new Vector3 (xOff, this.transform.position.y, zOff);

		if (_player.transform.position.x > this.transform.position.x)
			this.transform.position = new Vector3 (_player.transform.position.x, this.transform.position.y, zOff);

		if (_player.transform.position.y < this.transform.position.y)
			this.transform.position = new Vector3 (this.transform.position.x, yOff, zOff);

		if (_player.transform.position.y > this.transform.position.y)
			this.transform.position = new Vector3 (this.transform.position.x, _player.transform.position.y, zOff);

		
	}
}
