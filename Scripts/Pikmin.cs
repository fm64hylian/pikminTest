using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikmin : MonoBehaviour {


	public Sprite[] sprites;

	private int value = 1;
	private Sprite chosenSprite;
		

	// Use this for initialization
	void Start () {
		int randPikmin = Random.Range (0, 8);
		chosenSprite = sprites [randPikmin];

		if (randPikmin >= 0 && randPikmin < 3) { //leaf
			value = 1;
		} else if (randPikmin >= 3 && randPikmin < 6) { //bulb
			value = 2;
		} else if (randPikmin >= 6 && randPikmin < 9) {	//flower	
			value = 3;
		} 
		this.GetComponent<SpriteRenderer> ().sprite = chosenSprite;
	}


	public int getValue(){
		return value;
	}

	public Sprite getSprite(){
		return chosenSprite;
	}
}
