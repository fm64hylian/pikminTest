using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instructionsManager : MonoBehaviour {

	public GameObject image;
	Button buttonClose;

	// Use this for initialization
	void Awake () {		
		hideInstructions ();
	}
	
	public void showInstructions(){
		image.SetActive (true);

	}

	public void hideInstructions(){
		image.SetActive (false);
		//buttonClose.SetActive (false);
	}
}
