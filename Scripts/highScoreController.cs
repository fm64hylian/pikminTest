using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highScoreController : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Text textScore = GetComponent<UnityEngine.UI.Text> ();

		if (PlayerPrefs.HasKey ("Score")) {
			textScore.text = "High Score\n" + PlayerPrefs.GetInt ("Score");
		} else {
			PlayerPrefs.SetInt ("Score",0);
			textScore.text = "High Score\n"+ 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
