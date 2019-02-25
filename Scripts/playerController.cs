using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	public Camera cam;

	private float maxWid;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		if(cam == null){
			cam = Camera.main;
		}
		rb2d = GetComponent<Rigidbody2D>();
		Renderer rend = GetComponent<Renderer>();

		Vector3 uppercorner = new Vector3 (Screen.width, Screen.height, 0); //calcular boundaries de screen
		Vector3 targetWid = cam.ScreenToWorldPoint(uppercorner);
		maxWid = targetWid.x;
		float playerwid = rend.bounds.extents.x;	
		maxWid = targetWid.x - playerwid;
	}
	
	// Update is called once per phisics timestep
	void FixedUpdate () {
		Vector3 rawPos = cam.ScreenToWorldPoint(Input.mousePosition); //raw position de la camara
		Vector3 targetPos = new Vector3(rawPos.x,0.0f,0.0f); //solo se movera en eje x

		float targetWid = Mathf.Clamp (targetPos.x, -maxWid, maxWid); //clamp player, respeta boundaries de screen
		targetPos = new Vector3(targetWid, targetPos.y,targetPos.z);

		rb2d.MovePosition(targetPos);
	}

	public float getMaxwidth(){
		return maxWid;
	}
}
