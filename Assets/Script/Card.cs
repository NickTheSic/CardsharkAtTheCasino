using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Card", fileName = "New Card")]

public class Card : ScriptableObject {

	public new string name;
	public string description;

	public int upgrade;

	public int DeckNumber;// 0 is the upgrade cards, 1 - 52 will be suits

	public Sprite artwork;

	public RuntimeAnimatorController animation;

}
