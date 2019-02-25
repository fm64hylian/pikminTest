using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pikminManager : MonoBehaviour {
			
	public GameObject[] fallingStuff; //contiene a pikmin y bulborb
	public Camera cam;

	//UI
	public Text textTime;
	public GameObject textFinalScore;
	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject backButton;
	public GameObject highScoreText;
	private AudioSource audiopikmin;

	private float timeLeft;	
	private float spawnTime = 1.8f; 
	private float maxWid;

	void Awake(){
		audiopikmin = GetComponent<AudioSource> ();
	}

	// igual que player, se va a basar en las dimensiones de la pantalla
	void Start () {
		if(cam == null){
			cam = Camera.main;
		}
		timeLeft = 60;
		gameOverText.SetActive (false);
		restartButton.SetActive (false);
		backButton.SetActive (false);
		textFinalScore.SetActive (false);
		highScoreText.SetActive (false);

		//calcular boundaries de screen
		Vector3 uppercorner = new Vector3 (Screen.width, Screen.height, 0); 
		Vector3 targetWid = cam.ScreenToWorldPoint(uppercorner);
		maxWid = targetWid.x;
		float pikwid = fallingStuff[0].GetComponent<Renderer>().bounds.extents.x;	
		maxWid = targetWid.x - pikwid;

		//inicializar highscore
		int initialHighScore = PlayerPrefs.HasKey("Score") ? PlayerPrefs.GetInt("Score") :0;
		highScoreText.gameObject.GetComponent<UnityEngine.UI.Text> ().text = "High Score "+initialHighScore;

		updateTexttime ();
		StartCoroutine(spawnPikmin());
	}


	//fixedupdate deberia usarse solo en phisics pero meh
	void FixedUpdate(){
		timeLeft -= Time.deltaTime;
		if(timeLeft<0){
			timeLeft = 0;
		}
		//a los 10 segundos time cambia de color
		if (timeLeft < 10) {
			textTime.font.material.color = new Color (1.0f, 0.64f, 0.0f);
		} else {
			textTime.font.material.color =Color.white;
		}
		updateTexttime ();
	}

	//coroutine
	IEnumerator spawnPikmin(){

		float[] wave1Secs={(float)spawnTime*0.45f,spawnTime*0.8f}; //min y max de spawntime
		float[] wave2Secs={(float)spawnTime*0.2f,(float)spawnTime*0.45f};
		float[] wave3Secs={(float)spawnTime*0.01f,(float)spawnTime*0.2f};

		yield return new WaitForSeconds(1.5f); //pausa chiquitita antes de que empiece el spawn

		while(timeLeft > 0){ 
		Vector3 pos = new Vector3 (Random.Range (-maxWid, maxWid),transform.position.y,0.0f);		

		//disminuyendo la probabilidad de que un bulborb aparezca a 20%, respawn a los 35 segundos
		int randPikburb = Random.Range(0, timeLeft < 35 ? fallingStuff.Length +3 : 0); //+3
		//rand es entre 0 y 5, por lo tanto solo si vale 1 es bulborb, 0-5 pikmin
		randPikburb = randPikburb == 1 ? 1 : 0;

		//el quaternion debe rotar en el eje Z para que se vea en 2D
		Instantiate (fallingStuff[randPikburb],new Vector3(pos.x,pos.y),Quaternion.Euler(0, 0, Random.Range(0, 360)));

			// WAVE 1 (15 segundos)
		if(timeLeft > 45 && timeLeft <= 60){ 
			yield return new WaitForSeconds(Random.Range(wave1Secs[0],wave1Secs[1]));
			// WAVE 2 (25 segundos)
		}else if(timeLeft > 20 && timeLeft <= 45){
			yield return new WaitForSeconds(Random.Range(wave2Secs[0],wave2Secs[1]));	
			// WAVE 3 (20 segundos)
		}else if(timeLeft <=20 && timeLeft >=0){ 
			yield return new WaitForSeconds(Random.Range(wave3Secs[0],wave3Secs[1]));	
		}

	}

		//cuando termine el tiempo, esperar un poco y mostrar results
		yield return new WaitForSeconds(1.5f);

		//cuando termina dice pikmiiin
		audiopikmin.Play(0);
		gameOverText.SetActive (true);
		restartButton.SetActive (true);
		backButton.SetActive (true);
		textFinalScore.SetActive (true);
		getFinalScore();
		highScoreText.SetActive (true);	

	}

	void updateTexttime(){
		textTime.text = "Time\n" + Mathf.RoundToInt (timeLeft);				
	}

	void getFinalScore(){
		string strScore = textFinalScore.gameObject.GetComponent<UnityEngine.UI.Text> ().text;
		//sacar el valor de text
		string[] str = strScore.Split (' ');  //'' char "" string
		int finalScore = int.Parse(str [1]);

		//solo si es mayor a highscore, overwrite
		if(finalScore > PlayerPrefs.GetInt("Score")){			
			PlayerPrefs.SetInt ("Score",finalScore);	
		}
		highScoreText.gameObject.GetComponent<UnityEngine.UI.Text> ().text = "High Score " + PlayerPrefs.GetInt("Score");
	}
}
 