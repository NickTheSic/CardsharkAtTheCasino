using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameControl {

	//Basically controls upgrades right now

	public static bool hide;
	public static bool climb;
	public static bool swim;
	public static bool run;
	public static bool jump;
	public static bool highJump;

	public static int points = 0; 
	public static int guis = 0; 


	public static void Upgrading(int u, int deck){

		if (u == 0) {

			DeckPickup (deck);

		}

		///Edt
		if (u == 1) {
			run = true;
			guis = 1;
		}

		if (u == 2) {
			hide = true;
			guis = 2;
		}

		if (u == 3) {
			climb = true;
			guis = 3;
		}

		if (u == 4) {
			jump = true;
			guis = 4;
		}

		Debug.Log ("Upgraded; " + u);

	}
		
	public static bool[] CollectedDeck = new bool[53];

	//CHANGE THIS UP IN FUTURE
	public static int TotalCards = 12;
	public static int cardsCollected = 0;

	public static void DeckPickup(int SuitNum){

		if (SuitNum == 0) {
			
		}
		else {
			CollectedDeck [SuitNum] = true;
			cardsCollected++;
			Debug.Log("Cards collected: " + cardsCollected);

			for (int i = 0; i < CollectedDeck.Length; i++) {

				Debug.Log ("Card at " + i + "is " + CollectedDeck [i]);

			}
		}
	}


	public static void Restart(){

		hide = false;
		climb = false;
		swim = false;
		run = false;
		jump = false;
		highJump = false;

		points = 0; 
		guis = 0; 

		cardsCollected = 0;


	}

	/*public static bool SAce = false;
	public static bool STwo = false;
	public static bool SThree = false;*/

	public static bool isSpotted = false;

	public static void ResetSpotted(){

		isSpotted = false;

	}



}
