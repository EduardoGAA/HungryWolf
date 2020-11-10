using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlSoldado : MonoBehaviour {

	public float vel = -1f;
	Rigidbody2D rgb;
	Animator anim;
	public int energy = 100;
	public Slider slider;
	bool haciaDerecha = false;
	public GameObject bulletPrototype;
	CreacionFire crf;
    public Text txt;
	ControlWolf ctrw;
	AudioSource aSource;
	public AudioClip disparando;
	public AudioClip ouch;

	// Use this for initialization
	void Start () {
		
		rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		aSource = GetComponent<AudioSource> ();
		crf = FindObjectOfType<CreacionFire> ();
		ctrw = FindObjectOfType<ControlWolf> ();
		energy = 100;
	}

	// Update is called once per frame
	void Update () {

		if (energy <= 0) {
			energy = 0;
			anim.ResetTrigger ("disparar");
			anim.ResetTrigger ("caminar");
			anim.ResetTrigger ("idle");
			anim.SetTrigger ("morir");

			//if(anim.GetCurrentAnimatorStateInfo(2).Equals("Muriendo"))
				//anim.ResetTrigger("morir");

			//Destroy(gameObject, 1.5f);
			Destroy(GameObject.Find("jaula"),3);

			//Destroy (GameObject.Find("crf"), 3);
		}
		slider.value = energy;
		txt.text = energy.ToString ();
		return;
	}

	void FixedUpdate () {
		Vector2 v = new Vector2 (vel, 0);
		rgb.velocity = v;
		if (energy == 0) {
			vel = 0;
			//anim.gameObject.GetComponent<Animator> ().enabled = false;
		}
		/*if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Caminando") && Random.value < 1f / (60f * 3f)) {
			anim.SetTrigger ("idle");
		}
		else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
			if (Random.value < 1f / 3f ) {
				anim.SetTrigger ("disparar");
			} else {
				anim.SetTrigger ("caminar");
			}
		}*/
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name.Equals ("Wolf"))
			disparar ();
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

	public void disparar(){
		if(ctrw.energy > 0)
		anim.SetTrigger ("disparar");//idle
		//aSource.PlayOneShot (disparando);
		else return;
	}

	public void emitirBala(){
		GameObject bulletCopy = Instantiate (bulletPrototype);
		bulletCopy.transform.position = new Vector3(haciaDerecha?transform.position.x+2:transform.position.x-2, transform.position.y-1, -1) ; 
		bulletCopy.GetComponent<ControlBala> ().direction = new Vector3 (transform.localScale.x, 0, 0);
		energy--;
		aSource.PlayOneShot(disparando);
	}

	public void BajarPuntosPorPatada(){
		if(energy > 0){
		aSource.PlayOneShot (ouch);
		energy -=5;
		//flip();
		}
	}
	public void animarFuegos(){
		crf.destroyObject ();
	}
}
