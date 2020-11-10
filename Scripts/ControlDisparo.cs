using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDisparo : MonoBehaviour {
	//Collider2D disparandoA = null;
	public float probabilidadDeDisparo = 1f;

	ControlSoldado ctr;

	// Use this for initialization
	void Start () {
		ctr = GameObject.Find ("Enemigo").GetComponent<ControlSoldado> ();
	}

	/*void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name.Equals ("Wolf") && disparandoA == null) {
			decidaSiDispara (other);
			disparar();
		}
		if (other.gameObject.name.Equals ("Wolf") && disparandoA == null) {
			disparar ();
			disparandoA = other;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other == disparandoA){
			disparandoA = null;
		}
	}*/
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name.Equals("Wolf"))
			disparar();
	}

	/*void decidaSiDispara(Collider2D other){
		if (Random.value < probabilidadDeDisparo) {
			disparar ();
			disparandoA = other;
		}
	}*/

	void disparar(){
		ctr.disparar ();
	}
}
