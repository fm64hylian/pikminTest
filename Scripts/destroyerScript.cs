using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**Destruye todo ya que salio de la pantalla
 * */
public class destroyerScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){				
			Destroy (other.gameObject);	
	}
}
