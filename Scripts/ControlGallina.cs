using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGallina : MonoBehaviour {

	ControlWolf ctw;
	public float vel = -1f;
	Rigidbody2D rgb;
//	Animator anim;
	bool haciaDerecha = false;
	AudioSource aSource;
	public AudioClip cacareando;

	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
	//	anim = GetComponent<Animator> ();
		ctw = FindObjectOfType<ControlWolf> ();
		aSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		Vector2 v = new Vector2 (vel, 0);
		rgb.velocity = v;
		//if (energy == 0)
			//vel = 0;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name.Equals ("Wolf")) {
			aSource.PlayOneShot (cacareando);
			ctw.siguienteEscena ();
			Destroy (gameObject,1);

		}
		else
			flip ();
	}

	void flip(){
		vel *= -1;
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		haciaDerecha = !haciaDerecha;
	}
}
