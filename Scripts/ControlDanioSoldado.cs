using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDanioSoldado : MonoBehaviour {

	Collider2D colliderEnem = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name.Equals ("Enemigo") && colliderEnem == null) {
			Debug.Log ("Colisión con el enemigo!");
			colliderEnem = other;
			Invoke ("BajarPuntosEnemigo", 0);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other == colliderEnem) {
			Debug.Log ("Salir de colisión con el enemigo!");
			colliderEnem = null;
			CancelInvoke ("BajarPuntosEnemigo");
		}
	}

	void BajarPuntosEnemigo(){
		Debug.Log ("BajarPuntosEnemigo");
		colliderEnem.gameObject.GetComponent<ControlSoldado> ().BajarPuntosPorPatada ();
	}
}
