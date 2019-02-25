using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restartGame : MonoBehaviour {

	public void restart(){		
		SceneManager.LoadScene("mainScene");
	}

	public void BackToMenu(){
		SceneManager.LoadScene("titleMenu");
	}
}
