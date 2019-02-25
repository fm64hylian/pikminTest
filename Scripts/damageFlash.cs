using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageFlash : MonoBehaviour {
	private Material mat;
	private Color[] colors = {new Color(1.0f, 0.64f, 0.0f), Color.red}; //orange
	private AudioSource audioDamage;

	public void Awake(){
		mat = GetComponent<SpriteRenderer>().material;
		audioDamage = GetComponent<AudioSource> ();
	}

	public void startFlash(){
		StartCoroutine(Flash(0.2f, 0.04f));
	}

	/**
	 * time cuanto tiempo, interval que tan rapido
	 */ 
	IEnumerator Flash(float time, float intervalTime){
		float elapsedTime = 0f;
		int index = 0;
		//auch sound
		audioDamage.Play(0);
		while(elapsedTime < time ){
			mat.color = colors[index % 2];

			elapsedTime += Time.deltaTime;
			index++;
			yield return new WaitForSeconds(intervalTime);
		}
		//despues dejar como antes, el default de renderer es white
		mat.color = Color.white;
	}
}
