using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** maneja todas las las colisiones de olimar con pikmin y bulborb*/
public class olimarCollider : MonoBehaviour {

	private GameObject thisObj; //olimar
	//text UI
	public Text textScore;
	public Text textFinalScore;

	private int score =0;

	AudioSource audioSrc;
	Object[] myClips;

	void Awake(){ //cargamos los clips con soniditos random
		thisObj = this.gameObject;
		audioSrc =GetComponent<AudioSource>();
		myClips = Resources.LoadAll("pikminSounds",typeof(AudioClip));
	}


	void OnTriggerEnter2D(Collider2D other){

		//si no colisiono con olimar no suma puntos
		if (thisObj.CompareTag ("destroyer")) {			
			Destroy (other.gameObject);
			return;	
		}

		//pikmin
		if (other.gameObject.CompareTag ("pikmin")) { 
			getPikmin (other);
		}

		//si es olimar y bulborb
		if( thisObj.CompareTag ("olimar") && other.gameObject.CompareTag ("bulborb")){
			hitByBulborb(other);
		}	
		//updateHighScore ();
	}

	void getPikmin(Collider2D other){
		//sonidito random
		AudioClip clip = (AudioClip)myClips[Random.Range(0, myClips.Length-1)];
		audioSrc.PlayOneShot(clip);

		Pikmin pik = (Pikmin)other.GetComponent<Pikmin> ();
		score += pik.getValue ();
		Destroy (other.gameObject);
		Debug.Log ("score " + score);
		textScore.text = "Score\n"+score; //este score se muestra en la ui		
		textFinalScore.text = "Score "+score;
	}

	void hitByBulborb(Collider2D other){
		//cambio de color de olimar a rojo, eso no deberia ir aca tho
		thisObj.GetComponent<damageFlash>().startFlash();

		Destroy (other.gameObject);
		score = score - 5;
		score = score < 0 ? 0 : score; //si es menor a 0 no negativo
		textScore.text = "Score\n"+score; //este score se muestra en la ui
		textFinalScore.text = "Score "+score;
	}

	/*void updateHighScore(){
		if(score > PlayerPrefs.GetInt("Score")){
		PlayerPrefs.SetInt ("Score",score);
		}
	}*/
}
